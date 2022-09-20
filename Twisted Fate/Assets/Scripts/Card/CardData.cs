using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    private int _cardNumber;
    //0 rainbow, 1 health, 2 armour, 3 draw, 4 stun, 5 damage
    private int _cardType;

    public int cardType
    {
        get { return _cardType; }
        set { _cardType = value; }
    }

    public CardData(int number, int type)
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

    public int GetNumber()
    {
        return _cardNumber;
    }
}
