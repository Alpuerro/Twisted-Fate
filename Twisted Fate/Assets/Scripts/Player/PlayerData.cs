using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerDataObject", order = 1)]
public class PlayerData : ScriptableObject
{
    public int maxHealth;
    public int maxShield;
    public int handSize;
}
