using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHand : MonoBehaviour
{
    private HorizontalLayoutGroup _layoutGroup;
    private CardDeck _deck;
    private RectTransform _rectTransform;

    public int handSize = 7;
    public float angleOffset = 7;
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
        for (int i = 0; i < handSize; i++)
        {
            DrawCard();
        }
    }

    public void ReorderHand(CardData card = null)
    {
        if (_rectTransform.childCount > 1)
        {
            RectTransform cardTransform = _rectTransform.GetChild(0).transform as RectTransform;
            if (cardTransform.sizeDelta.x * _rectTransform.childCount > _rectTransform.sizeDelta.x)
            {
                _layoutGroup.spacing = -(cardTransform.sizeDelta.x * _rectTransform.childCount - _rectTransform.sizeDelta.x) / (_rectTransform.childCount + 1);
                _layoutGroup.padding.left = (int)_layoutGroup.spacing;
            }

            float angleSpan = angleOffset * _rectTransform.childCount;
            for (int i = 0; i < _rectTransform.childCount; i++)
            {
                _rectTransform.GetChild(i).GetComponent<Card>().SetCardRotation((angleSpan * 0.5f) - angleOffset * i);
            }
        }
    }

    [ContextMenu("Draw one card")]
    public void DrawCard()
    {
        if (_rectTransform.childCount < handSize)
        {
            Card newCard = _deck.DrawCard();
            newCard.transform.parent = transform;
            ReorderHand();
        }
    }
}
