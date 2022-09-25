using UnityEngine;

[CreateAssetMenu(fileName = "GameLoopData", menuName = "ScriptableObjects/GameLoopDataObject", order = 1)]
public class GameLoopData : ScriptableObject
{
    public int round;
    public int score;
}