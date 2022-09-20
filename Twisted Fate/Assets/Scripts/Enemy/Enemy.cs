using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Percentage of the level that is multiplied to the base damage stat.")]
    /// <summary> Percentage of the level that is multiplied to the base damage stat. </summary>
    private float _damagePerLevelMultiplier;

    public EnemyType enemyType;
    public int level = 1;
    public int health;
    public int defense;
    public int baseDamage;

    public int AttackDamage
    {
        get { return (int)(baseDamage * ((level * _damagePerLevelMultiplier) + 1)); }
    }
}

public enum EnemyType
{
    Small,
    Medium,
    Large
}

public enum EnemyActions
{
    Attack,
    Defend
}