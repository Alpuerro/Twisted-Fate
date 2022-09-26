using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    [SerializeField] DeckData _deckData;
    private CardGraveyard _graveyard;

    private Stack<CardData> _cardRemaings = new Stack<CardData>();

    private void Awake()
    {
        _graveyard = FindObjectOfType<CardGraveyard>();
        StartDeck();
    }

    private void StartDeck()
    {
        CreateNewDeck();
        ShuffleDeck();
    }

    private void CreateNewDeck()
    {
        for (int i = 0; i < _deckData.data.Length; i++)
        {
            _cardRemaings.Push(new CardData(i, _deckData.data[i]));
        }
    }

    [ContextMenu("Start game")]
    public void StartGame()
    {
        GameEvents.GameStart.Invoke();
    }

    public Card DrawCard()
    {
        Card newCard = CardPool.instance.GetNewCard();
        if (_cardRemaings.Count > 0) newCard.SetCardData(_cardRemaings.Pop());
        else newCard.SetCardData(new CardData(0, 0)); //only in case, it shouldnt happend
        // Debug.Log(_cardRemaings.Count);

        if (_cardRemaings.Count <= 1)
        {
            Debug.Log("Graveyard al deck");
            FindObjectOfType<CardInfoPanel>().AddCards(new List<CardData>(_graveyard.GetCards()));
            foreach (CardData c in _graveyard.GetCards())
            {
                _cardRemaings.Push(c);
            }
            _graveyard.ClearGraveyard();
            ShuffleDeck();
        }
        return newCard;
    }

    public void AddCards(List<CardData> cardsToAdd)
    {
        foreach (CardData c in cardsToAdd)
        {
            _cardRemaings.Push(c);
        }
    }

    public void AddCards(CardData card)
    {
        _cardRemaings.Push(card);
    }

    private void ShuffleDeck()
    {
        CardData[] auxArray = _cardRemaings.ToArray();

        for (int i = 0; i < auxArray.Length; i++)
        {
            int j = Random.Range(i, auxArray.Length);
            CardData aux = auxArray[i];
            auxArray[i] = auxArray[j];
            auxArray[j] = aux;
        }

        _cardRemaings = new Stack<CardData>(auxArray);
    }
}
