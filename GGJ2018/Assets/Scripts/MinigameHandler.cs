using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameHandler : MonoBehaviour {
    [SerializeField] private Scrollbar _horizontalScrollBar;
    [SerializeField] private GameObject _hitBox;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _chunks;
    [SerializeField] private float _scrollSpeed;

    private GameObject[] _letters = null;
    private List<GameObject> _chunkList = null;
    private RectTransform _hitBoxRect;
    private EventSystem _eventSystem;

    private GameObject _currentChunk;
    private int _currentChunkSize;

    private bool _startScrolling = false;
    private bool _chunkSelected = false;
    private int _index = 0;
    
    float keysHit;
    float keysTotal;
    private string uppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public List<GameObject> ChunkList {
        get { return _chunkList; }
    }

    // Use this for initialization
    void Start () {
        _eventSystem = GameObject.FindGameObjectWithTag("EvtSystem").GetComponent<EventSystem>();
        _letters = new GameObject[_content.transform.childCount];
        _chunkList = new List<GameObject>(_chunks.transform.childCount);

        for (int i = 0; i < _chunkList.Count; i++) {
            _chunkList[i] = _chunks.transform.GetChild(i).gameObject;
        }

        _hitBoxRect = _hitBox.GetComponent<RectTransform>();

    }
	
    private void RandomizeLetters() {
        for (int i = 0; i < _letters.Length; i++) {
            _letters[i] = _content.transform.GetChild(i).gameObject;

            string randomChar = uppercaseAlphabet[Random.Range(0, uppercaseAlphabet.Length)].ToString();
            _letters[i].GetComponent<Text>().text = randomChar;
        }
    }

    public void SelectChunk() {
        if (!_chunkSelected) {
            _currentChunk = _eventSystem.currentSelectedGameObject;
            _currentChunkSize = _currentChunk.GetComponent<ChunkScript>().GetSize();
            Debug.Log("chunk reselected "+_currentChunkSize);
            keysHit = 0;
            keysTotal = _letters.Length;//select correct chunk key list
            _chunkSelected = true;
            _startScrolling = true;
            RandomizeLetters();
        }
    }

	// Update is called once per frame
	void Update () {
        if (_chunkSelected) {
            Debug.Log("Calling chunky monkey");
            DisableChunkInteraction();
            ScrollText();
        }
    }

    private void DisableChunkInteraction() {
        if (_eventSystem.currentSelectedGameObject != null) {
            _eventSystem.SetSelectedGameObject(null);

            for (int i = 0; i < _chunkList.Count; i++) {
                Button chunk = _chunkList[i].GetComponent<Button>();
                chunk.interactable = false;
            }
        }
    }

    private void ScrollText() {
        if (_startScrolling) {
            _horizontalScrollBar.value += _scrollSpeed * _currentChunkSize * Time.deltaTime;

            if (_horizontalScrollBar.value == 1) {
                _startScrolling = false;
                _chunkSelected = false;
                _index = 0;
                _horizontalScrollBar.value = 0;
                //Debug.Log("keysHit - "+ keysHit +" || keysTotal - "+ keysTotal);
                _currentChunk.GetComponent<ChunkScript>().SetProcentage(keysHit / keysTotal);
                Destroy(_currentChunk);
                if (_chunks.transform.GetChild(1).gameObject != null) {
                    _eventSystem.SetSelectedGameObject(_chunks.transform.GetChild(1).gameObject);
                } else {
                    _eventSystem.SetSelectedGameObject(null);
                }

                for (int i = 0; i < _chunkList.Count; i++) {
                    Button chunk = _chunkList[i].GetComponent<Button>();
                    chunk.interactable = true;
                }
                
            } else {
                if (!HasEnded()) {
                    HandleKeyHit();
                } else {
                    // NOTE: The game is done here, but the text still has to scroll off screen, so wait for it
                    //       Once thats done _chunkSelected will be false, so if you need to check for that, that'll be a way to do it.
                    //       So you're only picking a chunk when _chunkSelected is false, the minigame will be done during this time.

                    Debug.Log("Minigame Ended. Waiting for scroll to finish.");
                }
            }
        }
    }
    
    private void HandleKeyHit() {
        GameObject letter = _letters[_index];

        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space)) {
            if (Input.GetKeyDown(_letters[_index].GetComponent<Text>().text.ToLower())) {
                if (_hitBoxRect.transform.position.x - 150.0f < letter.transform.position.x
                    && _hitBoxRect.transform.position.x + 100f > letter.transform.position.x) {
                    Debug.Log("Scored a point! How great..");
                    keysHit++;
                    _index++;
                } else if (_hitBoxRect.transform.position.x + 190.0f < letter.transform.position.x) {
                    Debug.Log("Pressed the button too early!");
                }
            } else {
                Debug.Log("Just pressed the wrong button");
            }
        }

        if (_hitBoxRect.transform.position.x - 80.0f > letter.transform.position.x) {
            Debug.Log("Pressed too late...");
            _index++;
        }
    }

    public bool HasEnded() {
        return (_index >= _content.transform.childCount);
    }
}
