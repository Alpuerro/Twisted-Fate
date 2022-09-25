using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Image[] healthBarFills;
    [SerializeField] Image[] healthBarFaders;
    [SerializeField] Image shieldBarFill;
    [SerializeField] Image shieldBarFader;
    [SerializeField] Image damageTint;


    int currentHealthBar = 2;
    public async Task UpdateHealthBar(float currentHealth)
    {
        StartCoroutine(AnimateBar(healthBarFaders[currentHealthBar], healthBarFills[currentHealthBar], currentHealth));
        await Task.Yield();
    }
    public async Task UpdateShield(float currentShield)
    {
        StartCoroutine(AnimateBar(shieldBarFader, shieldBarFill, currentShield));
        await Task.Yield();
    }

    public void DamageFeedback()
    {
        Sequence damageFeedback = DOTween.Sequence();
        damageFeedback.Append(transform.DOShakePosition(0.5f, 30));
        damageFeedback.Join(damageTint.DOFade(0.5f, 0.1f));
        damageFeedback.Append(damageTint.DOFade(0.0f, 0.2f));

        damageFeedback.Play();
    }

    public void SetHealthBar(int currentHealth, int maxHealth)
    {
        int healthPerBar = maxHealth / healthBarFills.Length;
        for (int i = 0; i < healthBarFills.Length; i++)
        {
            //0 index is the last health bar, the bottom one
            healthBarFills[i].fillAmount = Mathf.Clamp01(currentHealth / (healthPerBar * i+1));
            if (healthBarFills[i].fillAmount < 1) currentHealthBar = i;
            healthBarFaders[i].fillAmount = healthBarFills[i].fillAmount;
        }
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
