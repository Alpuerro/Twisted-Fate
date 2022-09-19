using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int _cardNumber;
    private int _cardType;

    public int cardType
    {
        get { return _cardType; }
        set { _cardType = value; }
    }

    public Card(int number, int type)
    {
        _cardNumber = number;
        _cardType = type;
    }

    public int GetNumberUnits()
    {
        return _cardNumber % 10;
    }

    public int GetNumberDecens()
    {
        return _cardNumber / 10;
    }
}
