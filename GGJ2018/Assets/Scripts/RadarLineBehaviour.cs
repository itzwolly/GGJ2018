using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RadarLineBehaviour : MonoBehaviour {
    [SerializeField] private GameObject _radarLine;
    [SerializeField] private GameObject _chunks;
    [SerializeField] private float _speed;

    int _size;

    public Sprite _size1;
    public Sprite _size2;
    public Sprite _size3;

    private RectTransform _maskRect;
    private RectTransform _lineRect;
    private Vector3 _startPosition;

	// Use this for initialization
	void Start () {
        _maskRect = GetComponent<RectTransform>();
        _lineRect = _radarLine.GetComponent<RectTransform>();
        _startPosition = _lineRect.anchoredPosition;
    }
	
	// Update is called once per frame
	void Update () {
            if (Mathf.Abs(_lineRect.anchoredPosition.y) < _maskRect.rect.height) {
                _radarLine.transform.position = new Vector3(_radarLine.transform.position.x, _radarLine.transform.position.y - _speed * Time.deltaTime, _radarLine.transform.position.z);
                DetectChunkCollision();
            } else {
                _lineRect.anchoredPosition = _startPosition;
            }
    }

    private void DetectChunkCollision() {
        for (int i = 0; i < _chunks.transform.childCount; i++) {
            GameObject chunk = _chunks.transform.GetChild(i).gameObject;
            RectTransform rectTransform = chunk.GetComponent<RectTransform>();
            ChunkScript script = chunk.GetComponent<ChunkScript>();

            if (_radarLine.transform.position.y > chunk.transform.position.y - rectTransform.rect.height / 2 + _lineRect.rect.height / 2 
                && _radarLine.transform.position.y < chunk.transform.position.y - _lineRect.rect.height / 2) {
                // Update chunky monkey sprite..
                // chunk.GetComponent<Image>().sprite = newSprite;
               
                if (!script.PassedOver)
                {
                    chunk.GetComponent<ChunkScript>().GrowSize();
                    Image img = chunk.GetComponent<Image>();
                    //img.color = new Color(img.color.r, img.color.g+50, img.color.b);
                    _size = chunk.GetComponent<ChunkScript>().GetSize();
                    if (_size == 1)
                    {
                        chunk.GetComponent<Image>().sprite = _size1;
                    }
                    if (_size == 2)
                    {
                        chunk.GetComponent<Image>().sprite = _size2;
                    }
                    if (_size == 3)
                    {
                        chunk.GetComponent<Image>().sprite = _size3;
                    }
                    script.PassedOver = true;
                }
            }
            else
            {
                script.PassedOver = false;
            }
        }
    }
}
