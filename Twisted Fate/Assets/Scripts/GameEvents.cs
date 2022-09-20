using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static UnityEvent GameStart = new UnityEvent();
    public static UnityEvent<CardData> CardPlayed = new UnityEvent<CardData>();
    public static UnityEvent<CardData> CardRemoved = new UnityEvent<CardData>();
}
