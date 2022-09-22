using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGraveyard : MonoBehaviour
{
    private Queue<CardData> _cards = new Queue<CardData>();
    [SerializeField] Image graveyardFill;

    public void AddCards(List<CardData> cards)
    {
        graveyardFill.gameObject.SetActive(true);
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
        graveyardFill.gameObject.SetActive(false);
        _cards.Clear();
    }
}
