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
    private Dictionary<int, bool> _roomStarted = new Dictionary<int, bool>();
    private Dictionary<TcpClient, string> _clientPlayer = new Dictionary<TcpClient, string>();
    private Dictionary<string, TcpClient> _playerClient = new Dictionary<string, TcpClient>();
    private Dictionary<TcpClient, float> _clientValues = new Dictionary<TcpClient, float>();
    private List<TcpClient> _clients = new List<TcpClient>();

    /*
     _roomsWithPlayers
     _clientReady
     _gameToPlayers
     _playerToGames
     _roomStarted
     _clientPlayer
     _playerClient
     _clients
         */
    private TcpListener _listener;
    private bool _serverStart;

    void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(transform.gameObject);
    }
    private void OnApplicationQuit()
    {
        foreach(TcpClient client in _clients)
        {
            BinaryWriter writer = new BinaryWriter(client.GetStream());
            SerializeDeserialize.Serialize(new ExitMessage(), writer);
        }
        _serverStart = false;
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
        _serverStart = true;
        while (_serverStart)
        {
            //Debug.Log("Handling Clients");
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
                    BinaryWriter writer = new BinaryWriter(client.GetStream());
                    SerializeDeserialize.Serialize(new ExitMessage(),writer);
                    Debug.Log("Client gave error, removing...");
                    Debug.Log(e.ToString());
                    string name = _clientPlayer[client];
                    int room = _playerToGames[name];

                    _roomsWithPlayers[room].Remove(client);
                    _clientReady.Remove(name);
                    _gameToPlayers[room].Remove(name);
                    _playerToGames.Remove(name);
                    _clientPlayer.Remove(client);
                    _playerClient.Remove(name);

                    _clients.RemoveAt(i);

                    client.GetStream().Close();
                    client.Close();
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
            Debug.Log(message);
            if(message is ExitMessage)
            {
                Debug.Log("disconnecting a clinet");
                string name = _clientPlayer[pClient];
                int room = _playerToGames[name];

                _roomsWithPlayers[room].Remove(pClient);
                _clientReady.Remove(name);
                _gameToPlayers[room].Remove(name);
                _playerToGames.Remove(name);
                _clientPlayer.Remove(pClient);
                _playerClient.Remove(name);

                _clients.Remove(pClient);

                pClient.GetStream().Close();
                pClient.Close();
            }
            else if (message is StringMessage)
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
                _playerClient.Add(connected.Name, pClient);
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
                    //else if (_roomStarted[_roomsWithPlayers.Keys.Count - 1])
                    //{
                    //    _roomsWithPlayers.Add(_roomsWithPlayers.Keys.Count, new List<TcpClient>());

                    //    _gameToPlayers.Add(_roomsWithPlayers.Keys.Count - 1, new List<string>());
                    //}
                    _roomsWithPlayers[_roomsWithPlayers.Keys.Count-1].Add(pClient);
                    //_playersWithRooms.Add(pClient, _roomsWithPlayers.Keys.Count - 1);
                    _playerToGames.Add(connected.Name, _roomsWithPlayers.Keys.Count - 1);
                    _gameToPlayers[_roomsWithPlayers.Keys.Count - 1].Add(connected.Name);
                }
                _clientReady.Add(connected.Name, false);
                _roomsText = _roomsWithPlayers.Keys.Count.ToString();
                //Debug.Log(_gameToPlayers.Keys.Count);
                List<string> pNames = new List<string>();
                List<bool> pReadys = new List<bool>();

                foreach(string s in _gameToPlayers[_roomsWithPlayers.Keys.Count - 1])
                {
                    BinaryWriter tewriter = new BinaryWriter(_playerClient[s].GetStream());
                    SerializeDeserialize.Serialize(new OtherPlayerConnectedToLobby(connected.Name, false),tewriter);
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


                int count = 0;
                foreach (string s in _gameToPlayers[room])
                    if (_clientReady[s])
                        count++;
                bool start = _gameToPlayers[room].Count / 2 < count;
                _roomStarted[room] = start;
                Debug.Log(_roomStarted[room]+"room has started");
                foreach (TcpClient client in _roomsWithPlayers[room])
                {
                    BinaryWriter secwriter = new BinaryWriter(client.GetStream());
                    SerializeDeserialize.Serialize(new ReadyUpMessageResponse(clientName, b), secwriter);
                    if (start)
                    {
                        count--;
                        _clientReady[_clientPlayer[client]] = false;
                        SerializeDeserialize.Serialize(new StartGameMessage(), secwriter);
                    }
                }
                _readyText =count.ToString();
            }
        }

        //sending to client positions of gameobjects

    }
}
