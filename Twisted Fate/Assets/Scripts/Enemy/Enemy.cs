using EnemyInfo;
using System.Threading.Tasks;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Space(10)]
    public string enemyType;
    public EnemyData enemyData;
    public int level = 1;
    public int health;
    public int shield;
    public bool isStunned;
    public int numberOfTurnsStunned = 0;
    private EnemyAction action;
    [Space(10)]
    public EnemyUIManager uIManager;

    public int AttackDamage { get { return (int)(enemyData.attack * ((level * enemyData._damagePerLevelMultiplier) + 1)); } }
    public int ShieldAmmountToAdd { get { return (int)(enemyData.defense * ((level * enemyData._defensePerLevelMultiplier) + 1)); } }

    private void Start() { enemyType = enemyData.name; }

    public void SetEnemyData(in EnemyData enemyData, in int level)
    {
        this.enemyData = enemyData;
        enemyType = enemyData.name;
        this.level = level;
        health = this.enemyData.maxHealth;
        shield = 0;
        isStunned = false;
        numberOfTurnsStunned = 0;
    }

    public async Task Show()
    {
        await Task.Yield();
    }

    public void TakeDamage(in int damage)
    {
        if (damage <= shield) shield -= damage;
        else health -= damage;

        Task task = UpdateUI();
    }

    public bool IsAlive() { return health <= 0 ? false : true; }

    public void Stun(int numberOfTurnsStunned)
    {
        isStunned = true;
        if (this.numberOfTurnsStunned < enemyData.maxAccumulatedStuns)
            this.numberOfTurnsStunned += numberOfTurnsStunned;
    }

    public void AddShield()
    {
        if (shield < enemyData.maxShield) shield += ShieldAmmountToAdd;
        Task task = UpdateUI();
    }

    public ref EnemyAction GetAction() 
    {
        return ref action; 
    }
    public void SetAction()
    {
        if (!isStunned)
        {
            action.enemyActionsType = enemyData.GetActionType();
            switch (action.enemyActionsType)
            {
                case EnemyActionTypes.Attack:
                    action.ammout = AttackDamage;
                    //0 es el icono de atacar
                    uIManager.ShowIcon(0);
                    break;
                case EnemyActionTypes.Defend:
                    action.ammout = ShieldAmmountToAdd;
                    //1 es el icono de atacar
                    uIManager.ShowIcon(1);
                    break;
                default:
                    action.ammout = 0;
                    break;
            }
        }
        else 
        {
            uIManager.ShowIcon(2);
        }
    }

    public void SetUI()
    {
        uIManager.SetHealthBar(health, enemyData.maxHealth);
        uIManager.SetShieldBar(shield, enemyData.maxShield);
    }

    public async Task UpdateUI()
    {
        await uIManager.UpdateHealthBar((float)health / enemyData.maxHealth);
        await uIManager.UpdateShield((float)shield / enemyData.maxShield);
        await Task.Yield();
    }
}
