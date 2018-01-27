using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetTextScript : MonoBehaviour {
    [SerializeField] Text _textBox;

    public Text GetTextBox()
    {
        return _textBox;
    }
}
