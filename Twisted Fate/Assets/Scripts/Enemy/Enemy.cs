using EnemyInfo.EnemyAction;
using System.Threading.Tasks;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    public int level = 1;
    public bool isStunned;
    public int numberOfTurnsStunned = 0;
    private EnemyAction enemyAction;

    public int AttackDamage { get { return (int)(enemyData.attack * ((level * enemyData._damagePerLevelMultiplier) + 1)); } }
    public int ShieldAmmountToAdd { get { return (int)(enemyData.defense * ((level * enemyData._defensePerLevelMultiplier) + 1)); } }

    public void SetEnemyData(EnemyData eData) => enemyData = eData;

    public async Task Show()
    {
        await Task.Yield();
    }

    /// <returns>True if its still alive.</returns>
    public bool TakeDamage(in int dmg)
    {
        if (dmg <= enemyData.shield) enemyData.shield -= dmg;
        else if (enemyData.health <= dmg) return false;
        else enemyData.health -= dmg;

        return true;
    }

    public void Stun(int numberOfTurnsStunned)
    {
        isStunned = true;
        this.numberOfTurnsStunned += numberOfTurnsStunned;
        //max of turns to be stunned, this should be externalzed
        this.numberOfTurnsStunned = Mathf.Clamp(this.numberOfTurnsStunned, 0, 3);
    }

    public void AddShield() { enemyData.shield += ShieldAmmountToAdd; }

    public ref EnemyAction GetAction() { return ref enemyAction; }
    public void SetAction()
    {
        enemyAction.enemyActionsType = ((Random.value < 0.5) ? EnemyActionsType.Attack : EnemyActionsType.Defend);
        switch (enemyAction.enemyActionsType)
        {
            case EnemyActionsType.Attack:
                enemyAction.ammout = AttackDamage;
                break;
            case EnemyActionsType.Defend:
                enemyAction.ammout = ShieldAmmountToAdd;
                break;
            default:
                enemyAction.ammout = 0;
                break;
        }
    }
}
