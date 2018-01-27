using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using DLLLibrary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientController : MonoBehaviour
{
    [SerializeField] string _server = "localhost";
    [SerializeField] int _port = 55555;
    [SerializeField] string _playerName;
    [SerializeField] bool _createNewGame;

    [SerializeField] string _sceneToLoad;
    [SerializeField] GameObject _playerIndicatorHolder;
    [SerializeField] GameObject _playerIndicatorBrush;

    static Thread gameThred;
    static TcpClient client;
    static NetworkStream stream;

    static BinaryReader reader;
    static BinaryWriter writer;

    Message msg;
    private bool _inLobby;
    private bool _connected;
    private bool _isStillRunning;
    private bool _workingOnMsg = false;

    private Dictionary<string, float> _players;
    private Dictionary<string, bool> _readyPlayers;

    private void StartHacking()
    {
        //SceneManager.LoadSceneAsync(_sceneToLoad);
    }

    public void ReadyUp()
    {
        SerializeDeserialize.Serialize(new ReadyUpMessage(_playerName), writer);
        //SceneManager.LoadSceneAsync(_sceneToLoad);
    }


    void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(transform.gameObject);
    }


    void Begin()
    {
        while (true)
        {
            if (_inLobby)
                break;
            try
            {
                client = new TcpClient(_server, _port);
                Debug.Log("Joining new server");
                _inLobby = true;
                stream = client.GetStream();
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);

                gameThred = new Thread(StartGame);
                gameThred.Start();
                _isStillRunning = true;
            }
            catch
            {
                Debug.Log("No server available");
            }
        }
        Debug.Log("connected to lobby");
        return;
    }

    // Use this for initialization
    void Start () {
        Thread thread = new Thread(Begin);
        thread.Start();
    }
    void Update()
    {
        if (_connected)
        {

            if (msg != null)
            {
                //Debug.Log("Checking the message: " + msg);
                if (msg is StringMessage)
                {
                    Debug.Log("Received String: " + (msg as StringMessage).Message);
                }
                else if (msg is ReadyUpMessageResponse)
                {
                    ReadyUpMessageResponse readymsg = (msg as ReadyUpMessageResponse);
                    ReadyPlayer(readymsg.Name,readymsg.Val);
                }
                else if (msg is ConnectedToLobby)
                {

                }
                else if (msg is OtherPlayerConnectedToLobby)
                {

                }
                msg = null;
                _workingOnMsg = false;
            }
        }

    }

    private void ReadyPlayer(string who,bool val)
    {
        _readyPlayers[who] = val;
    }

    //if time edit to list not separate message checks
    private void StartGame(object obj)
    {
        SerializeDeserialize.Serialize(new ConnectToServerMessage(_playerName,_createNewGame), writer);
        while (true)
        {
            if (_isStillRunning)
            {
                if (!_workingOnMsg)
                {
                    msg = SerializeDeserialize.Deserialize(reader);
                    _workingOnMsg = true;
                }
            }
            else
            {
                break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _isStillRunning = false;
        //gameThred.Abort();
    }

}
