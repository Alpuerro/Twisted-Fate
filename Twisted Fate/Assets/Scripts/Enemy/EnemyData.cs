using EnemyInfo;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    [Space(10)]
    public int maxHealth;
    public int maxShield;
    public int attack;
    public int defense;
    public int maxAccumulatedStuns;

    [Space(10)]
    [Range(0, 1)]
    public float _damagePerLevelMultiplier;
    [Range(0, 1)]
    public float _defensePerLevelMultiplier;
    [Range(0, 1)]
    public float _attackProbability;

    [Space(10)]
    public Texture2D enemySprite;
    public Texture2DArray actionIcons;
    public Animator animator;

    public EnemyActionTypes GetActionType()
    {
        return ((Random.value < _attackProbability) ? EnemyActionTypes.Attack : EnemyActionTypes.Defend);
    }
}

namespace EnemyInfo
{
    public enum EnemyActionTypes { Attack, Defend }
    public struct EnemyAction
    {
        public int ammout;
        public EnemyActionTypes enemyActionsType;
    }
}