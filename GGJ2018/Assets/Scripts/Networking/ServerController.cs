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
    [SerializeField] float _copIncrease;
    [SerializeField] Text _connected;
    string _connectedText;
    [SerializeField] Text _ready;
    string _readyText="0";
    [SerializeField] Text _rooms;
    string _roomsText;

    private Thread _messageThread;
    //private Dictionary<int, List<TcpClient>> _roomsWithPlayers = new Dictionary<int, List<TcpClient>>();
    //private Dictionary<TcpClient, int> _playersWithRooms = new Dictionary<TcpClient, int>();
    private Dictionary<string, bool> _clientReady = new Dictionary<string, bool>();

    //private Dictionary<int, List<string>> _gameToPlayers = new Dictionary<int, List<string>>();
    //private Dictionary<string, int> _playerToGames = new Dictionary<string, int>();
    private Dictionary<int, bool> _roomStarted = new Dictionary<int, bool>();
    private Dictionary<TcpClient, string> _clientPlayer = new Dictionary<TcpClient, string>();
    private Dictionary<string, TcpClient> _playerClient = new Dictionary<string, TcpClient>();
    private Dictionary<TcpClient, float> _clientValues = new Dictionary<TcpClient, float>();
    private List<TcpClient> _clients = new List<TcpClient>();

    public TcpClient _first=null;
    public TcpClient _second=null;

    private float _copValues;
    private int count;
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
    private bool _chunkUpdate=false;

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

    void SendUpdate()
    {
        Debug.Log("there are clinets - "+_clients.Count);
        foreach(TcpClient tClient in _clients)
        {
            //Debug.Log("sending UJpdaye");
            BinaryWriter twriter = new BinaryWriter(tClient.GetStream());
            float firstVal, secondVal;
            string nextName = _clientPlayer[_first];
            firstVal = _clientValues[tClient];
            secondVal = _clientValues[_first];
            if (_first == tClient)
            {
                if (_second == null)
                {
                    nextName = "You are solo";
                    secondVal = 0;
                }
                else
                {
                    nextName = _clientPlayer[_second];
                    secondVal = _clientValues[_second];
                }
                firstVal = _clientValues[tClient];
            }
            float[] bars = { firstVal, secondVal, _copValues };
            Debug.Log("first"+firstVal+"second"+secondVal+"third"+_copValues);
            SerializeDeserialize.Serialize(new ProgressBarInfo(bars, 3, nextName), twriter);
        }
    }

    private void FixedUpdate()
    {
        if (_roomStarted.Keys.Count > 0 && _roomStarted[0] == true)
        {
            count++;
            if (count > 100 || _chunkUpdate)
            {
                SendUpdate();
                count = 0;
                _chunkUpdate = false;
            }
            _copValues += _copIncrease / 100;
        }
        _ready.text = _readyText;
        _connected.text = _connectedText;
        _rooms.text = _roomsText;
    }

    // Update is called once per frame
    void Update ()
    {
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
                    int room = 0;

                    //_roomsWithPlayers[room].Remove(client);
                    _clientReady.Remove(name);
                    //_gameToPlayers[room].Remove(name);
                    //_playerToGames.Remove(name);
                    _clientPlayer.Remove(client);
                    _playerClient.Remove(name);

                    _clients.RemoveAt(i);
                    _clientValues.Remove(client);
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
            //Debug.Log(message);
            if (message is ExitMessage)
            {
                Debug.Log("disconnecting a clinet");
                string name = _clientPlayer[pClient];
                int room = 0;

                //_roomsWithPlayers[room].Remove(pClient);
                _clientReady.Remove(name);
                //_gameToPlayers[room].Remove(name);
                //_playerToGames.Remove(name);
                _clientPlayer.Remove(pClient);
                _playerClient.Remove(name);

                _clients.Remove(pClient);
                _clientValues.Remove(pClient);
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
                if (_first == null)
                    _first = pClient;
                else if (_second == null)
                    _second = pClient;

                _clientValues.Add(pClient, 0);
                _connectedText = _clients.Count.ToString();
                ConnectToServerMessage connected = message as ConnectToServerMessage;
                
                _clientPlayer.Add(pClient, connected.Name);
                _playerClient.Add(connected.Name, pClient);
                if (connected.NewGame)
                {
                    Debug.Log("new game");
                    //_roomsWithPlayers.Add(_roomsWithPlayers.Keys.Count, new List<TcpClient>());
                    //_roomsWithPlayers[_roomsWithPlayers.Keys.Count - 1].Add(pClient);

                    //_playerToGames.Add(connected.Name, _roomsWithPlayers.Keys.Count - 1);
                    //_gameToPlayers.Add(_roomsWithPlayers.Keys.Count - 1, new List<string>());
                    //_gameToPlayers[_roomsWithPlayers.Keys.Count - 1].Add(connected.Name);
                }
                else
                {
                    //if (_roomsWithPlayers.Count==0)
                    //{
                    //    _roomsWithPlayers.Add(0,new List<TcpClient>());
                    //    //_roomsWithReady.Add(0, new List<string>());
                    //    _gameToPlayers.Add(0, new List<string>());
                    //}
                    //else if (_roomStarted[_roomsWithPlayers.Keys.Count - 1])
                    //{
                    //    _roomsWithPlayers.Add(_roomsWithPlayers.Keys.Count, new List<TcpClient>());

                    //    _gameToPlayers.Add(_roomsWithPlayers.Keys.Count - 1, new List<string>());
                    //}
                    //_roomsWithPlayers[_roomsWithPlayers.Keys.Count-1].Add(pClient);
                    ////_playersWithRooms.Add(pClient, _roomsWithPlayers.Keys.Count - 1);
                    //_playerToGames.Add(connected.Name, _roomsWithPlayers.Keys.Count - 1);
                    //_gameToPlayers[_roomsWithPlayers.Keys.Count - 1].Add(connected.Name);
                }
                _clientReady.Add(connected.Name, false);
                _roomsText = 1.ToString();
                //Debug.Log(_gameToPlayers.Keys.Count);
                List<string> pNames = new List<string>();
                List<bool> pReadys = new List<bool>();

                foreach(TcpClient s in _clients)
                {
                    BinaryWriter tewriter = new BinaryWriter(s.GetStream());
                    SerializeDeserialize.Serialize(new OtherPlayerConnectedToLobby(connected.Name, false),tewriter);
                    pNames.Add(_clientPlayer[s]);
                    pReadys.Add(_clientReady[_clientPlayer[s]]);
                }
                SerializeDeserialize.Serialize(new ConnectedToLobby(pNames,pReadys), twriter);
            }
            else if(message is ReadyUpMessage)
            {
                string clientName = _clientPlayer[pClient];
                int room = 0;
                
                bool b = _clientReady[clientName];
                _clientReady[clientName] = !(b);
                b = !b;


                int count = 0;
                foreach (string s in _playerClient.Keys)
                    if (_clientReady[s])
                        count++;
                bool start = _clients.Count / 2 < count;
                _roomStarted[room] = start;
                //Debug.Log(_roomStarted[room]+ "room has started");
                foreach (TcpClient client in _clients)
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
            else if(message is ChunkCompletionInfo)
            {
                ChunkCompletionInfo info = message as ChunkCompletionInfo;
                Debug.Log(info.Procent+" | "+info.Size + " | "+info.Type);
                if(info.Type)//normal chunk
                {
                    _clientValues[pClient] += info.Procent * info.Size / 300;
                }
                else if(pClient!=_first)//steal chunk
                {
                    float dif = _clientValues[_first] - _clientValues[pClient];

                    _clientValues[pClient] += dif / 2 * (info.Procent * info.Size / 3);
                    _clientValues[_first] -= dif / 3 * (info.Procent * info.Size / 3);
                }

                if(_clientValues[pClient]>_clientValues[_first])
                {
                    _second = _first;
                    _first = pClient;
                }
                _chunkUpdate = true;
            }
        }

        //sending to client positions of gameobjects

    }
}
