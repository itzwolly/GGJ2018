using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RadarLineBehaviour : MonoBehaviour {
    [SerializeField] private GameObject _radarLine;
    [SerializeField] private GameObject _chunks;
    [SerializeField] private float _speed;

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

            if (_radarLine.transform.position.y > chunk.transform.position.y - rectTransform.rect.height / 2 + _lineRect.rect.height / 2 
                && _radarLine.transform.position.y < chunk.transform.position.y - _lineRect.rect.height / 2) {
                // Update chunky monkey sprite..
                // chunk.GetComponent<Image>().sprite = newSprite;
                chunk.GetComponent<Image>().color = new Color(Mathf.Sin(Time.time), Mathf.Sin(Time.time), Mathf.Sin(Time.time));
            }
        }
    }
}
