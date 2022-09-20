using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComboEffect
{
    public int comboType;
    public int comboLevel;

    public ComboEffect(int type, int level)
    {
        comboLevel = level;
        comboType = type;
    }
}

public class ComboCardManager : MonoBehaviour
{
    public static ComboCardManager instance;
    public ComboData comboValues;

    private CardHand _hand;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else 
        {
            Destroy(this);
        }
    }

    public void ProcessComboCard(List<CardData> cards)
    {
        //check damage combo
        int damageAmount = ProcessDamage(cards);
        Debug.Log("Damages a hacer: " + damageAmount);

        //check effect combo
        List<ComboEffect> comboEffects = ProcessCombo(cards);
        foreach (ComboEffect c in comboEffects)
        {
            switch (c.comboType)
            {
                case 0:
                    ProcessRainbowCombo();
                    break;
                case 1:
                    ProcessHealthUpCombo(comboValues.healthUpData[c.comboLevel-1]);
                    break;
                case 2:
                    ProcessArmourUpCombo(comboValues.armourUpData[c.comboLevel - 1]);
                    break;
                case 3:
                    ProcessDrawCombo(comboValues.drawData[c.comboLevel - 1]);
                    break;
                case 4:
                    ProcessStunCombo(comboValues.stunData[c.comboLevel - 1]);
                    break;
                case 5:
                    ProcessDamageUpCombo(comboValues.damageUpData[c.comboLevel - 1]);
                    break;
            }
        }
    }

    private void ProcessRainbowCombo()
    {
        ProcessHealthUpCombo(comboValues.healthUpData[0]);
        ProcessArmourUpCombo(comboValues.armourUpData[0]);
        ProcessDrawCombo(comboValues.drawData[0]);
        ProcessStunCombo(comboValues.stunData[0]);
        ProcessDamageUpCombo(comboValues.damageUpData[0]);
    }

    private void ProcessHealthUpCombo(int healthUpAmount)
    {
        //TODO hacer que el personaje se suba la vida
        Debug.Log("Vida subida");
    }

    private void ProcessArmourUpCombo(int armourUpAmount)
    {
        //TODO hacer que el personaje gane armadura
        Debug.Log("Armadura subida");
    }

    private void ProcessDrawCombo(int cardsToDraw)
    {
        if (_hand == null) _hand = FindObjectOfType<CardHand>();

        for (int i = 0; i < cardsToDraw; i++)
        {
            _hand.DrawCard();    
        }
        Debug.Log("Cartas robadas");
    }

    private void ProcessStunCombo(int turnsToStun)
    {
        //TODO hacer que el enemigo se stunee
        Debug.Log("Stuniado");
    }

    private void ProcessDamageUpCombo(float damageUpPercentaje)
    {
        //TODO hacer que el jugador gane mas daño
        Debug.Log("Daño subido");
    }

    private int ProcessDamage(List<CardData> cards)
    {
        int amount = 0;
        int numberUnits = 0;
        int lastUnits = 0;
        int lastAmount = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            numberUnits = 0;
            amount = 0;
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[j].GetNumberUnits() == cards[i].GetNumberUnits())
                {
                    numberUnits++;
                    amount += cards[j].GetNumberDecens();
                }
            }
            if (lastAmount < amount && numberUnits >= 2)
            {
                lastAmount = amount;
            }
            if(lastUnits < numberUnits)
            {
                lastUnits = numberUnits;
            }
        }

        if (lastUnits >= 2)
            return lastAmount;
        else 
            return 0;
    }

    private List<ComboEffect> ProcessCombo(List<CardData> cards)
    {
        cards = cards.OrderByDescending(c => c.cardType).ToList();
        List<ComboEffect> comboEffects = new List<ComboEffect>();

        int lastType = -1;
        int comboLevel = -1;
        for (int i = 0; i < cards.Count; i++)
        {
            int newType = cards[i].cardType;
            if (newType == lastType) continue;

            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[j].cardType == newType)
                {
                    comboLevel++;
                }
            }

            if (comboLevel >= 1 || (newType == 0)) 
            {
                comboEffects.Add(new ComboEffect(newType, comboLevel));
            }

            lastType = newType;
            comboLevel = -1;
        }

        return comboEffects;
    }
}
