using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] Image healthBarFill;
    [SerializeField] Image healthBarFader;
    [SerializeField] Image shieldBarFill;
    [SerializeField] Image shieldBarFader;
    [SerializeField] GameObject[] actionIcons;
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
        healthBarFill.fillAmount = currentHealth / maxHealth;
        healthBarFader.fillAmount = healthBarFill.fillAmount;
    }

    public void SetShieldBar(int currentShield, int maxShield)
    {
        shieldBarFill.fillAmount = currentShield / maxShield;
        shieldBarFader.fillAmount = shieldBarFill.fillAmount;
    }

    public void ShowIcon(int actionIndex)
    {
        actionIcons[actionIndex].SetActive(true);
        HideIcons(actionIndex);
    }

    private void HideIcons(int iconNotToHide)
    {
        for (int i = 0; i < actionIcons.Length; i++)
        {
            if (i == iconNotToHide) continue;
            actionIcons[i].SetActive(false);
        }
    }

    private IEnumerator AnimateBar(Image barFade, Image barFill, float fillAmount)
    {
        barFill.fillAmount = fillAmount;
        yield return new WaitForSeconds(0.4f);

        barFade.fillAmount = barFill.fillAmount;

        yield return null;
    }
}
