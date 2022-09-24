using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
    public float tiemBetweenIcons = 0.2f;

    [SerializeField] Transform iconsParent;
    [SerializeField] ComboIcon damageIcon;
    [SerializeField] ComboIcon healthUpIcon;
    [SerializeField] ComboIcon armourUpIcon;
    [SerializeField] ComboIcon drawIcon;
    [SerializeField] ComboIcon stunIcon;
    [SerializeField] ComboIcon damageUpIcon;

    private CardHand _hand;

    int _damageAmount;
    List<ComboEffect> _comboEffects = new List<ComboEffect>();
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
        _damageAmount = ProcessDamage(cards);
        Debug.Log("Damages a hacer: " + _damageAmount);

        //check effect combo
        _comboEffects = ProcessCombo(cards);

        StartCoroutine(ComboAnimation());
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
        CreateIcon(healthUpIcon, healthUpAmount.ToString());
        Player.instance.HealthUp(healthUpAmount);
        Debug.Log("Vida subida");
    }

    private void ProcessArmourUpCombo(int armourUpAmount)
    {
        CreateIcon(armourUpIcon, armourUpAmount.ToString());
        Player.instance.ArmourUp(armourUpAmount);
        Debug.Log("Armadura subida");
    }

    private void ProcessDrawCombo(int cardsToDraw)
    {
        if (_hand == null) _hand = FindObjectOfType<CardHand>();
        CreateIcon(drawIcon, cardsToDraw.ToString());

        Player.instance.DrawCards(cardsToDraw);
        Debug.Log("Cartas robadas");
    }

    private void ProcessStunCombo(int turnsToStun)
    {
        CreateIcon(stunIcon, turnsToStun.ToString());
        Player.instance.StunEnemy(turnsToStun);
        Debug.Log("Stuniado");
    }

    private void ProcessDamageUpCombo(float damageUpPercentaje)
    {
        CreateIcon(damageUpIcon, damageUpPercentaje.ToString());
        Player.instance.DamageUp(damageUpPercentaje);
        Debug.Log("Daño subido");
    }

    private void ProcessDamageCombo()
    {
        if (_damageAmount > 0)
        {
            CreateIcon(damageIcon, _damageAmount.ToString());
        }
    }

    private void CreateIcon(ComboIcon iconPrefab, string text)
    {
        ComboIcon icon = Instantiate(iconPrefab, iconsParent);
        icon.CreateIcon(text);
    }

    private IEnumerator ComboAnimation()
    {
        ProcessDamageCombo();

        yield return new WaitForSeconds(tiemBetweenIcons);

        foreach (ComboEffect c in _comboEffects)
        {
            switch (c.comboType)
            {
                case 0:
                    ProcessRainbowCombo();
                    break;
                case 1:
                    ProcessHealthUpCombo(comboValues.healthUpData[c.comboLevel - 1]);
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

            yield return new WaitForSeconds(tiemBetweenIcons);
        }
        //TODO hacer que se aplique el multiplicador de daño
        GameLoop.Instance.nextDamageAmount = _damageAmount;

        yield return new WaitForSeconds(0.3f);
        ResetCombos();
        yield return null;
    }

    private void ResetCombos()
    {
        _damageAmount = 0;
        GameLoop.Instance.nextDamageAmount = -1;
        _comboEffects.Clear();
        foreach (ComboIcon c in iconsParent.GetComponentsInChildren<ComboIcon>())
        {
            c.DestroyIcon();
        }

        StopAllCoroutines();
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
