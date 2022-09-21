using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _textBoxes;
    [SerializeField] Image background;
    private CardData _data;

    public Vector3 scaleToReset = Vector3.one;

    public CardData GetCardData()
    {
        return _data;
    }

    public void SetCardRotation(float angle, bool instant = false)
    {
        if (instant)
        {
            background.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else {
            background.transform.DORotate(new Vector3(0, 0, angle), 0.3f);
        }

        ResetAnimation();
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

    public void CardPlayedAnimation()
    {
        scaleToReset = Vector3.one * 0.8f;
        background.transform.DOScale(Vector3.one * 0.8f, 0.35f).SetEase(Ease.InOutSine);
    }    

    public void CardRemovedAnimation()
    {
        background.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.InOutSine);
    }

    public void SelectedAnimation()
    {
        scaleToReset = background.transform.localScale;
        background.transform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    public void ResetAnimation()
    {
        background.transform.DOScale(scaleToReset, 0.2f);
    }
}
