using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;
using DLLLibrary;


public class ServerController : MonoBehaviour {
    [SerializeField] Text _connected;
    string _connectedText;
    [SerializeField] Text _ready;
    string _readyText="0";
    [SerializeField] Text _rooms;
    string _roomsText;

    private Thread _messageThread;
    private Dictionary<int, List<TcpClient>> _roomsWithPlayers = new Dictionary<int, List<TcpClient>>();
    //private Dictionary<TcpClient, int> _playersWithRooms = new Dictionary<TcpClient, int>();
    private Dictionary<string, bool> _clientReady = new Dictionary<string, bool>();

    private Dictionary<int, List<string>> _gameToPlayers = new Dictionary<int, List<string>>();
    private Dictionary<string, int> _playerToGames = new Dictionary<string, int>();
    private Dictionary<TcpClient, string> _clientPlayer = new Dictionary<TcpClient, string>();
    private List<TcpClient> _clients = new List<TcpClient>();


    private TcpListener _listener;
    private bool _gameStarted;

    void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(transform.gameObject);
    }
    private void OnApplicationQuit()
    {
        _gameStarted = false;
    }

    // Use this for initialization
    void Start ()
    {
        _listener = new TcpListener(IPAddress.Any, 55555);
        _listener.Start();
        _messageThread = new Thread(StartServer);
        _messageThread.Start();

    }
	
	// Update is called once per frame
	void Update ()
    {
        _ready.text = _readyText;
        _connected.text = _connectedText;
        _rooms.text = _roomsText;
        checkForNewClients();
    }
    private void StartServer(object obj)
    {
        _gameStarted = true;
        while (_gameStarted)
        {
            Debug.Log("Handling Clients");
            handleClients();
            Thread.Sleep(16);
        }
    }
    private void checkForNewClients()
    {
        //POLLING before blocking
        while (_listener.Pending())
        {
            Debug.Log("Accepting client...");

            TcpClient client = _listener.AcceptTcpClient();
            _clients.Add(client);

            //BinaryWriter tWriter = new BinaryWriter(client.GetStream());
            //_gameStarted = true;
        }
    }
    private void handleClients()
    {
        for (int i = _clients.Count - 1; i >= 0; i--)
        {
            TcpClient client = _clients[i];

            //POLLING before blocking
            if (client.Available > 0)
            {
                try
                {
                    handleClient(client);
                }
                catch (Exception e)
                {
                    Debug.Log("Client gave error, removing...");
                    Debug.Log(e.ToString());
                    client.GetStream().Close();
                    client.Close();
                    _gameToPlayers[_playerToGames[_clientPlayer[_clients[i]]]].Remove(_clientPlayer[_clients[i]]);
                    _playerToGames.Remove(_clientPlayer[_clients[i]]);
                    _clientPlayer.Remove(_clients[i]);
                    _clients.RemoveAt(i);
                    Debug.Log("" + _clients.Count + " clients left...");
                }
            }
        }
    }
    private void handleClient(TcpClient pClient)
    {
        NetworkStream stream = pClient.GetStream();
        BinaryReader reader = new BinaryReader(stream);
        Message message = SerializeDeserialize.Deserialize(reader);

        BinaryWriter twriter = new BinaryWriter(stream);
        if (message == null)
        {
            _clients.Remove(pClient);
            pClient.GetStream().Close();
            pClient.Close();
            Debug.Log("Aborted this client.");
        }
        else
        {
            if (message is StringMessage)
            {
                Debug.Log("received: " + (message as StringMessage).Message);
                foreach (TcpClient client in _clients)
                {
                    SerializeDeserialize.Serialize(message, twriter);
                }
            }
            else if (message is ConnectToServerMessage)
            {
                _connectedText = _clients.Count.ToString();
                ConnectToServerMessage connected = message as ConnectToServerMessage;
                
                _clientPlayer.Add(pClient, connected.Name);
                if (connected.NewGame)
                {
                    Debug.Log("new game");
                    _roomsWithPlayers.Add(_roomsWithPlayers.Keys.Count, new List<TcpClient>());
                    _roomsWithPlayers[_roomsWithPlayers.Keys.Count - 1].Add(pClient);

                    _playerToGames.Add(connected.Name, _roomsWithPlayers.Keys.Count - 1);
                    _gameToPlayers.Add(_roomsWithPlayers.Keys.Count - 1, new List<string>());
                    _gameToPlayers[_roomsWithPlayers.Keys.Count - 1].Add(connected.Name);
                }
                else
                {
                    if (_roomsWithPlayers.Count==0)
                    {
                        _roomsWithPlayers.Add(0,new List<TcpClient>());
                        //_roomsWithReady.Add(0, new List<string>());
                        _gameToPlayers.Add(0, new List<string>());
                    }
                    _roomsWithPlayers[_roomsWithPlayers.Keys.Count-1].Add(pClient);
                    //_playersWithRooms.Add(pClient, _roomsWithPlayers.Keys.Count - 1);
                    _playerToGames.Add(connected.Name, _roomsWithPlayers.Keys.Count - 1);
                    _gameToPlayers[_roomsWithPlayers.Keys.Count - 1].Add(connected.Name);
                }
                _clientReady.Add(connected.Name, false);
                _roomsText = _roomsWithPlayers.Keys.Count.ToString();
                Debug.Log(_gameToPlayers.Keys.Count);
                List<string> pNames = new List<string>();
                List<bool> pReadys = new List<bool>();

                foreach(string s in _gameToPlayers[_roomsWithPlayers.Keys.Count - 1])
                {
                    pNames.Add(s);
                    pReadys.Add(_clientReady[s]);
                }
                SerializeDeserialize.Serialize(new ConnectedToLobby(pNames,pReadys), twriter);
            }
            else if(message is ReadyUpMessage)
            {
                string clientName = _clientPlayer[pClient];
                int room = _playerToGames[clientName];
                bool b = _clientReady[clientName];
                _clientReady[clientName] = !(b);
                b = !b;

                foreach(TcpClient client in _roomsWithPlayers[room])
                {
                    BinaryWriter secwriter = new BinaryWriter(client.GetStream());
                    SerializeDeserialize.Serialize(new ReadyUpMessageResponse(clientName,b), secwriter);
                }
               _readyText =(_clientReady.Keys.Count).ToString();
            }
        }

        //sending to client positions of gameobjects

    }
}
