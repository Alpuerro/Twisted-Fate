using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    [Space(10)]
    public int health;
    public int attack;
    public int defense;
    public int shield;

    [Space(10)]
    [Range(0, 1)]
    [Tooltip("Percentage of the level that is multiplied to the base damage stat.")]
    /// <summary> Percentage of the level that is multiplied to the base damage stat. </summary>
    public float _damagePerLevelMultiplier;
    [Range(0, 1)]
    [Tooltip("Percentage of the level that is multiplied to the base defense stat.")]
    /// <summary> Percentage of the level that is multiplied to the base defense stat. </summary>
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