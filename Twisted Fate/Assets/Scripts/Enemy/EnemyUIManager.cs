using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] Slider healthBarFill;
    [SerializeField] Slider shieldBarFill;

    public void UpdateHealthBar(int currentHealth) => healthBarFill.value = currentHealth;
    public void UpdateShield(int currentShield) => shieldBarFill.value = currentShield;

    public void SetHealthBar(int currentHealth, int maxHealth)
    {
        healthBarFill.maxValue = maxHealth;
        healthBarFill.value = currentHealth;
    }

    public void SetShieldBar(int currentShield, int maxShield)
    {
        shieldBarFill.maxValue = maxShield;
        shieldBarFill.value = currentShield;
    }
}
