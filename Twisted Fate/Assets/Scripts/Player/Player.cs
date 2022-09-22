using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public PlayerData playerData;
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

    //TODO todas las animaciones y la comunicacion con la interfaz

    public void DamagePlayer(int amount)
    {
        health -= amount;

        if (health <= 0) Die();
    }

    private void Die()
    { 
        
    }

    public void DealDamageToEnemy(int damage)
    {
        //TOdO pegar putaso al enemigo
    }

    public void HealthUp(int amount)
    {
        health += amount;

        health = Mathf.Clamp(health, 0, playerData.maxHealth);
    }

    public void ArmourUp(int amount)
    {
        armour += amount;
    }

    public void DrawCards(int amount)
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

    public void StunEnemy(int amount)
    {
        //TODO stuniar enemigo
    }

    public void DamageUp(float percentajeAmount)
    {
        damageMultiplier += percentajeAmount;
    }
}