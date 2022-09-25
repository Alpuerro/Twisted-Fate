using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardHand : MonoBehaviour
{
    private HorizontalLayoutGroup _layoutGroup;
    private CardDeck _deck;
    private RectTransform _rectTransform;

    public int handSize = 0;
    public float angleOffset = 7;
    public bool _playable = false;
    void Start()
    {
        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
        _deck = FindObjectOfType<CardDeck>();
        _rectTransform = transform as RectTransform;

        GameEvents.GameStart.AddListener(StartGame);
        GameEvents.CardPlayed.AddListener(ReorderHand);
        GameEvents.CardRemoved.AddListener(ReorderHand);
    }

    private void StartGame()
    {
        //draw five cards at start
        for (int i = 0; i < 5; i++)
        {
            if (handSize < 7)
            {
                DrawCard();
            }
        }
    }

    public void ReorderHand(CardData card = null)
    {
        if (_rectTransform.childCount > 1)
        {
            handSize = _rectTransform.childCount;
            RectTransform cardTransform = _rectTransform.GetChild(0).transform as RectTransform;
            if (cardTransform.sizeDelta.x * _rectTransform.childCount > _rectTransform.sizeDelta.x)
            {
                _layoutGroup.spacing = -(cardTransform.sizeDelta.x * _rectTransform.childCount - _rectTransform.sizeDelta.x) / (_rectTransform.childCount + 1);
                _layoutGroup.padding.left = (int)_layoutGroup.spacing;
            }

            float angleSpan = angleOffset * _rectTransform.childCount;
            for (int i = 0; i < _rectTransform.childCount; i++)
            {
                Transform cardChild = _rectTransform.GetChild(i);
                float angleAmount = (angleSpan * 0.5f) - angleOffset * i;
                cardChild.GetComponent<Card>().SetCardRotation(angleAmount);
                //cardChild.GetChild(0).localPosition = new Vector3(cardChild.GetChild(0).localPosition.x, cardChild.GetChild(0).localPosition.y - angleAmount, cardChild.GetChild(0).localPosition.z);
            }
        }
    }

    [ContextMenu("Draw one card")]
    public void DrawCard()
    {
        if (handSize < 7)
        {
            handSize++;
            Card newCard = _deck.DrawCard();
            Sequence sequence = DOTween.Sequence();

            float middlePoint = newCard.transform.position.x + (transform.position.x - newCard.transform.position.x) / 2;
            sequence.Append(newCard.transform.DOMove(transform.position, 1.3f).SetEase(Ease.InOutSine));
            sequence.Join(newCard.transform.DOMoveY(transform.position.y + 100, 0.6f).SetEase(Ease.InSine));
            sequence.Join(newCard.transform.DOMoveY(transform.position.y, 0.7f).SetEase(Ease.OutSine).SetDelay(0.6f));
            sequence.AppendCallback(() =>
            {
                newCard.transform.SetParent(transform, false);
                ReorderHand();
            });
        }
    }
}
