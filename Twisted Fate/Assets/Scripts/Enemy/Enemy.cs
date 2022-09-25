using EnemyInfo.EnemyAction;
using System.Threading.Tasks;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public EnemyUIManager uIManager;

    public int level = 1;
    public int health;
    public int shield;
    public bool isStunned;
    public int numberOfTurnsStunned = 0;
    private EnemyAction enemyAction;

    public int AttackDamage { get { return (int)(enemyData.attack * ((level * enemyData._damagePerLevelMultiplier) + 1)); } }
    public int ShieldAmmountToAdd { get { return (int)(enemyData.defense * ((level * enemyData._defensePerLevelMultiplier) + 1)); } }

    public void SetEnemyData(in EnemyData enemyData, in int level)
    {
        this.enemyData = enemyData;
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
        this.numberOfTurnsStunned += numberOfTurnsStunned;
        this.numberOfTurnsStunned = Mathf.Clamp(this.numberOfTurnsStunned, 0, enemyData.maxAccumulatedStuns);
    }

    public void AddShield()
    {
        shield += ShieldAmmountToAdd;
        Task task = UpdateUI();
    }

    public ref EnemyAction GetAction() { return ref enemyAction; }
    public void SetAction()
    {
        enemyAction.enemyActionsType = ((Random.value < 0.5) ? EnemyActionsType.Attack : EnemyActionsType.Defend);
        switch (enemyAction.enemyActionsType)
        {
            case EnemyActionsType.Attack:
                enemyAction.ammout = AttackDamage;
                //0 es el icono de atacar
                uIManager.ShowIcon(0);
                break;
            case EnemyActionsType.Defend:
                enemyAction.ammout = ShieldAmmountToAdd;
                //1 es el icono de atacar
                uIManager.ShowIcon(1);
                break;
            default:
                enemyAction.ammout = 0;
                break;
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
