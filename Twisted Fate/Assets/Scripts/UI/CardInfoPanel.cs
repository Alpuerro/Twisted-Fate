using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CardInfoPanel : MonoBehaviour
{
    [SerializeField] DeckData deckData;
    [SerializeField] Button closeButton;
    [SerializeField] Button openButton;
    [SerializeField] TextMeshProUGUI[] cardByNumberTexts;
    [SerializeField] TextMeshProUGUI[] cardByTypeTexts;

    Vector3 startPosition;
    int[] numberCardsRemaining;
    int[] typeCardsRemaining;

    private void Start()
    {
        numberCardsRemaining = new int[10];
        typeCardsRemaining = new int[5];
        for (int i = 0; i < deckData.data.Length; i++)
        {
            int j = i % 10;
            if(i != 0) 
            {
                numberCardsRemaining[j]++;
                typeCardsRemaining[deckData.data[i]-1]++;
            }
        }
        startPosition = transform.localPosition;
        GameEvents.ComboPlayed.AddListener(ChangeInfo);
    }

    public void ShowInfoPanel()
    {
        Sequence open = DOTween.Sequence();
        open.Append(transform.DOLocalMoveX(startPosition.x - 443f, 1.0f).SetEase(Ease.InOutSine));
        open.AppendCallback(() =>
        {
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        });
        open.Play();
    }
    public void HideInfoPanel()
    {
        Sequence close = DOTween.Sequence();
        close.Append(transform.DOLocalMoveX(startPosition.x, 1.0f).SetEase(Ease.InOutSine));
        close.AppendCallback(() =>
        {
            openButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        });
        close.Play();
    }

    private void ChangeInfo(List<CardData> cardsPlayed)
    {
        foreach (CardData c in cardsPlayed)
        {
            numberCardsRemaining[c.GetNumberUnits()]--;
            typeCardsRemaining[c.cardType]--;
        }

        SetTexts();
    }

    private void AddCards(List<CardData> cardsReturned)
    {
        foreach (CardData c in cardsReturned)
        {
            numberCardsRemaining[c.GetNumberUnits()]++;
            typeCardsRemaining[c.cardType]++;
        }

        SetTexts();
    }

    private void SetTexts()
    { 
        
    }
}
