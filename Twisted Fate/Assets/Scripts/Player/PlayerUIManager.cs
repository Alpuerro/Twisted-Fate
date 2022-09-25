using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Image healthBarFill;
    [SerializeField] Image healthBarFader;
    [SerializeField] Image shieldBarFill;
    [SerializeField] Image shieldBarFader;

    public async Task UpdateHealthBar(float currentHealth)
    {
        StartCoroutine(AnimateBar(healthBarFader, healthBarFill, currentHealth));
        await Task.Yield();
    }
    public async Task UpdateShield(float currentShield)
    {
        StartCoroutine(AnimateBar(shieldBarFader, shieldBarFill, currentShield));
        await Task.Yield();
    }

    public void SetHealthBar(int currentHealth, int maxHealth)
    {
        healthBarFill.fillAmount = currentHealth/maxHealth;
        healthBarFader.fillAmount = healthBarFill.fillAmount;
    }

    public void SetShieldBar(int currentShield, int maxShield)
    {
        shieldBarFill.fillAmount = currentShield/maxShield;
        shieldBarFader.fillAmount = shieldBarFill.fillAmount;
    }

    private IEnumerator AnimateBar(Image barFade, Image barFill, float fillAmount)
    {
        barFill.fillAmount = fillAmount;
        yield return new WaitForSeconds(0.4f);

        barFade.fillAmount = barFill.fillAmount;

        yield return null;
    }
}
