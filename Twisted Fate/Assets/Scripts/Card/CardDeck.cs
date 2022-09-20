using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] Card cardPrefab;

    private Stack<CardData> _cardRemaings = new Stack<CardData>();

    private void Awake()
    {
        StartDeck();
    }

    private void StartDeck()
    {
        CreateNewDeck();
        ShuffleDeck();
    }

    private void CreateNewDeck()
    { 
        for(int i = 0; i < 101; i++)
        {
            if (i == 0)
            {
                //rainbow card
                _cardRemaings.Push(new CardData(i, 0));
                continue;
            }
            if ((i >= 1 && i <=19))
            {
                //health card
                _cardRemaings.Push(new CardData(i, 1));
                continue;
            }
            if ((i >=20 && i <= 39))
            {
                //armour card
                _cardRemaings.Push(new CardData(i, 2));
                continue;
            }
            if ((i >= 40 && i <= 59))
            {
                //draw card
                _cardRemaings.Push(new CardData(i, 3));
                continue;
            }
            if ((i >= 60 && i <= 79))
            {
                //stun card
                _cardRemaings.Push(new CardData(i, 4));
                continue;
            }
            if ((i >= 80 && i <= 100))
            {
                //damage card
                _cardRemaings.Push(new CardData(i, 5));
                continue;
            }
        }
    }

    [ContextMenu("Start game")]
    public void StartGame()
    {
        GameEvents.GameStart.Invoke();
    }

    public Card DrawCard()
    {
        Card newCard = Instantiate(cardPrefab, transform);
        newCard.SetCardData(_cardRemaings.Pop());
        Debug.Log(_cardRemaings.Count);

        //TODO: si no quedan cartas en el deck coger la de la pila de descartes

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
