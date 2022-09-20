using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _textBoxes;
    private CardData _data;

    public CardData GetCardData()
    {
        return _data;
    }

    public void SetCardData(CardData newData)
    {
        _data = newData;
        foreach (TextMeshProUGUI t in _textBoxes)
        {
            t.text = _data.GetNumber().ToString();
        }
    }
}
