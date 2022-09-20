using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _textBoxes;
    [SerializeField] Image background;
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
        switch (_data.cardType)
        {
            case 0:
                background.color = Color.grey;
                break;
            case 1:
                background.color = Color.green;
                break;
            case 2:
                background.color = Color.yellow;
                break;
            case 3:
                background.color = Color.cyan;
                break;
            case 4:
                background.color = Color.blue;
                break;
            case 5:
                background.color = Color.red;
                break;
        }
    }
}
