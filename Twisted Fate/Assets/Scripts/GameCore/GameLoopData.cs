using UnityEngine;

[CreateAssetMenu(fileName = "GameLoopData", menuName = "ScriptableObjects/GameLoopDataObject", order = 1)]
public class GameLoopData : ScriptableObject
{
    public int round;
    public int maxScore;

    public void UpdateScore(int score) { if (maxScore < score) maxScore = score; }
}