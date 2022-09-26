using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public static CardPool instance;
    [SerializeField] Card _cardPrefab;
    [SerializeField] int poolSize;

    Queue<Card> cardPool = new Queue<Card>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Card c = Instantiate(_cardPrefab, transform);
            cardPool.Enqueue(c);
            c.gameObject.SetActive(false);
        }
    }

    public Card GetNewCard() 
    {
        Card c = cardPool.Dequeue();
        c.gameObject.SetActive(true);
        return c;
    }

    public void ReturnCard(Card c)
    {
        cardPool.Enqueue(c);
        c.transform.parent = transform;
        c.gameObject.SetActive(false);
    }
}
