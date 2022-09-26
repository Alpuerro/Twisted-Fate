using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public static Player instance;

    public PlayerData playerData;
    public PlayerUIManager uIManager;
    [Space(10)]
    public int health;
    public int armour;
    public float damageMultiplier = 1;

    [Space(10)]
    public CardHand cardHand;
    public int cardsToDraw;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (SharedDataManager.GetDataByKey("firstRound") != null && (bool)SharedDataManager.GetDataByKey("firstRound") == false)
        {
            health = (int)SharedDataManager.GetDataByKey("currentHealth");
        }
        else
        {
            health = playerData.maxHealth;
        }

        if (uIManager == null) uIManager = FindObjectOfType<PlayerUIManager>();

        uIManager.SetHealthBar(health, playerData.maxHealth);
        uIManager.SetShieldBar(armour, playerData.maxShield);
    }

    public async Task UpdateUIBars()
    {
        await uIManager.UpdateHealthBar(health, playerData.maxHealth);
        await uIManager.UpdateShield((float)armour / playerData.maxShield);
        await Task.Yield();
    }

    public void DamagePlayer(in int amount)
    {
        int shield = armour;
        int damageAmount = amount;
        armour -= amount;
        armour = Mathf.Clamp(armour, 0, playerData.maxShield);
        damageAmount -= shield;
        damageAmount = Mathf.Clamp(damageAmount, 0, amount);
        health -= damageAmount;
        if (uIManager == null) uIManager = FindObjectOfType<PlayerUIManager>();
        uIManager.DamageFeedback();
        uIManager.SetShieldBar(armour, playerData.maxShield);
        uIManager.SetHealthBar(health, playerData.maxHealth);

        if (health <= 0) Die();
    }

    private void Die()
    {

    }

    public void HealthUp(in int amount)
    {
        health += amount;

        health = Mathf.Clamp(health, 0, playerData.maxHealth);
        uIManager.SetHealthBar(health, playerData.maxHealth);
    }

    public void ArmourUp(in int amount)
    {
        armour += amount;
        uIManager.SetShieldBar(armour, playerData.maxShield);
    }

    public void DrawCards(in int amount)
    {
        StartCoroutine(DrawCardProcess(amount));
    }

    private IEnumerator DrawCardProcess(int amount)
    {
        int currentDrawCards = 0;

        while (currentDrawCards < amount && cardHand.handSize < playerData.handSize)
        {
            cardHand.DrawCard();
            currentDrawCards++;

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    public void StunEnemy(in int amount)
    {
        GameLoop.Instance.enemy.Stun(amount);
    }

    public void DamageUp(in float percentajeAmount)
    {
        damageMultiplier += percentajeAmount;
    }
}