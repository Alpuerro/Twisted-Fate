using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    [Space(10)]
    public int maxHealth;
    public int attack;
    public int defense;
    public int maxAccumulatedStuns;

    [Space(10)]
    [Range(0, 1)]
    public float _damagePerLevelMultiplier;
    [Range(0, 1)]
    public float _defensePerLevelMultiplier;

    [Space(10)]
    public Texture2D enemySprite;
    public Texture2DArray actionIcons;
    public Animator animator;
}

namespace EnemyInfo
{
    namespace EnemyAction
    {
        public enum EnemyActionsType { Attack, Defend }
        public struct EnemyAction
        {
            public int ammout;
            public EnemyActionsType enemyActionsType;
        }
    }
}