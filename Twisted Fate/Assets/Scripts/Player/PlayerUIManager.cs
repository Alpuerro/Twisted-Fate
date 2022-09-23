using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Image healthBarFill;
    [SerializeField] TextMeshProUGUI armourText;
    [SerializeField] IncreaseIcon armourUpIcon;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateArmour(int amount)
    {
        armourText.text = amount.ToString();
        armourUpIcon.PlayAnimation();
    }
}
