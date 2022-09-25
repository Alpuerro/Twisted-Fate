using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/DeckData")]
public class DeckData : ScriptableObject
{
    //storage type and array index is the number of the card
    public int[] data = new int[101];
}
