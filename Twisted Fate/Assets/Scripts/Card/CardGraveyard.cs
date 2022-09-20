using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGraveyard : MonoBehaviour
{
    private Queue<CardData> _cards = new Queue<CardData>();

    public void AddCards(List<CardData> cards)
    {
        foreach (CardData c in cards)
        {
            _cards.Enqueue(c);
        }
    }

    public Queue<CardData> GetCards()
    {
        return _cards;
    }

    public void ClearGraveyard()
    {
        _cards.Clear();
    }
}
