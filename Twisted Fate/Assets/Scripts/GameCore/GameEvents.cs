using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static UnityEvent GameStart = new UnityEvent();
    public static UnityEvent UpdateUI = new UnityEvent();
    public static UnityEvent<CardData> CardPlayed = new UnityEvent<CardData>();
    public static UnityEvent<List<CardData>> ComboPlayed = new UnityEvent<List<CardData>>();
    public static UnityEvent<CardData> CardRemoved = new UnityEvent<CardData>();
    public static UnityEvent muteEvent = new UnityEvent();
    public static UnityEvent unmuteEvent = new UnityEvent();
    public static UnityEvent changeVolumeEvent = new UnityEvent();
}
