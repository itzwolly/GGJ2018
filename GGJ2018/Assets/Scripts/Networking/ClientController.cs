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
    [SerializeField] Color _readyColor;
    [SerializeField] Color _notReadyColor;


    private GameObject _playerIndicator;

    static Thread gameThred;
    static TcpClient client;
    static NetworkStream stream;

    static BinaryReader reader;
    static BinaryWriter writer;

    public static BinaryReader GetReader()
    {
        return reader;
    }
    public static BinaryWriter GetWriter()
    {
        return writer;
    }
    public static TcpClient GetClient()
    {
        return client;
    }

    Message msg;
    private bool _inLobby;
    private bool _connected;
    private bool _isStillRunning;
    private bool _workingOnMsg = false;

    private Dictionary<string, float> _players = new Dictionary<string, float>();
    private Dictionary<string, bool> _readyPlayers = new Dictionary<string, bool>();
    private Dictionary<string, GameObject> _readyGameObject = new Dictionary<string, GameObject>();

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
        //Debug.Log(_connected);
        if (_inLobby || _connected)
        {
            if (msg != null)
            {
                if(msg is ExitMessage)
                {
                    Debug.Log("disconnected by server");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else if (msg is StringMessage)
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
                    ConnectedToLobby con = msg as ConnectedToLobby;
                    Debug.Log(con.Names.Count);
                    for(int i=0;i<con.Names.Count;i++)
                    {
                        try
                        {
                            _readyPlayers.Add(con.Names[i], con.Readys[i]);
                            GameObject temp = Instantiate(_playerIndicatorBrush);
                            temp.transform.SetParent(_playerIndicatorHolder.transform);
                            temp.GetComponent<GetTextScript>().GetTextBox().text = con.Names[i];
                            _readyGameObject.Add(con.Names[i], temp);
                            ReadyPlayer(con.Names[i], con.Readys[i]);
                        }
                        catch
                        {
                            Debug.Log("player is already in list");
                        }
                    }
                }
                else if (msg is OtherPlayerConnectedToLobby)
                {
                    Debug.Log("Received other connection");
                    OtherPlayerConnectedToLobby con = msg as OtherPlayerConnectedToLobby;
                    GameObject temp = Instantiate(_playerIndicatorBrush);
                    temp.transform.SetParent(_playerIndicatorHolder.transform);
                    temp.GetComponent<GetTextScript>().GetTextBox().text = con.Name;
                    _readyPlayers.Add(con.Name, con.Ready);
                    _readyGameObject.Add(con.Name, temp);
                    ReadyPlayer(con.Name, con.Ready);

                }
                else if(msg is StartGameMessage)
                {
                    SceneManager.LoadSceneAsync(_sceneToLoad);
                }
                //Debug.Log("Checking the message: " + msg);
                msg = null;
                _workingOnMsg = false;
            }
        }

    }

    private void ReadyPlayer(string who,bool val)
    {
        _readyPlayers[who] = val;

        if(val)
        {
            _readyGameObject[who].GetComponent<Image>().color = _readyColor;
        }
        else
        {
            _readyGameObject[who].GetComponent<Image>().color = _notReadyColor;
        }
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
        SerializeDeserialize.Serialize(new ExitMessage(), writer);
        //gameThred.Abort();
    }

}
