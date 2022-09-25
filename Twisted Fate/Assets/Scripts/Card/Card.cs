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
    [SerializeField] Sprite[] cardSprites;
    [SerializeField] RectTransform visualParent;
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
            visualParent.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else {
            visualParent.DORotate(new Vector3(0, 0, angle), 0.3f);
        }

        ResetAnimation();
    }

    public void SetCardData(CardData newData)
    {
        _data = newData;
        foreach (TextMeshProUGUI t in _textBoxes)
        {
            t.text = "<color=#96e8ff>"+_data.GetNumberDecens().ToString()+ "</color>"+_data.GetNumberUnits().ToString();
        }
        background.sprite = cardSprites[_data.cardType];
    }

    public void CardPlayedAnimation()
    {
        scaleToReset = Vector3.one * 0.8f;
        visualParent.DOScale(Vector3.one * 0.8f, 0.35f).SetEase(Ease.InOutSine);
    }    

    public void CardRemovedAnimation()
    {
        visualParent.DOScale(Vector3.one, 0.35f).SetEase(Ease.InOutSine);
    }

    public void SelectedAnimation()
    {
        scaleToReset = background.transform.localScale;
        visualParent.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    public void ResetAnimation()
    {
        visualParent.DOScale(scaleToReset, 0.2f);
    }
}
