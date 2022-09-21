using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    [Space(10)]
    public EnemyType enemyType;
    [Space(10)]
    public int level = 1;
    public int health;
    public int attack;
    public int defense;
    public int shield;
    public bool isStunned;
    private EnemyAction enemyAction;

    [Space(10)]
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Percentage of the level that is multiplied to the base damage stat.")]
    /// <summary> Percentage of the level that is multiplied to the base damage stat. </summary>
    private float _damagePerLevelMultiplier;
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Percentage of the level that is multiplied to the base defense stat.")]
    /// <summary> Percentage of the level that is multiplied to the base defense stat. </summary>
    private float _defensePerLevelMultiplier;

    [Space(10)]
    public Texture2D enemySprite;
    public Texture2DArray actionIcons;
    public Animator animator;

    public int AttackDamage { get { return (int)(attack * ((level * _damagePerLevelMultiplier) + 1)); } }
    public int ShieldAmmount { get { return (int)(defense * ((level * _defensePerLevelMultiplier) + 1)); } }

    public ref EnemyAction GetEnemyAction()
    {
        enemyAction.enemyActionsType = ((Random.value < 0.5) ? EnemyActionsType.Attack : EnemyActionsType.Defend);
        switch (enemyAction.enemyActionsType)
        {
            case EnemyActionsType.Attack:
                enemyAction.ammout = AttackDamage;
                break;
            case EnemyActionsType.Defend:
                enemyAction.ammout = ShieldAmmount;
                break;
            default:
                enemyAction.ammout = 0;
                break;
        }

        return ref enemyAction;
    }
}

public struct EnemyAction
{
    public int ammout;
    public EnemyActionsType enemyActionsType;
}

public enum EnemyType { Small, Medium, Large }
public enum EnemyActionsType { Attack, Defend }