using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Image[] healthBarFills;
    [SerializeField] Image[] healthBarFaders;
    [SerializeField] Image shieldBarFill;
    [SerializeField] Image shieldBarFader;
    [SerializeField] Image damageTint;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI spText;


    int currentHealthBar = 2;
    public async Task UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float barAmount = 0.0f;
        currentHealth -= (maxHealth / healthBarFills.Length) * currentHealthBar;
        barAmount = (float)currentHealth / (float)(maxHealth / healthBarFills.Length);
        StartCoroutine(AnimateBar(healthBarFaders[currentHealthBar], healthBarFills[currentHealthBar], barAmount));
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

    public void SetRoundText(int roundNumber)
    {
        roundText.text = "ROUND <color=#FFFAE6>\r\n<size=120%>" + roundNumber + "</color>";
    }

    public void SetHealthBar(int currentHealth, int maxHealth)
    {
        int healthPerBar = maxHealth / healthBarFills.Length;
        if (currentHealth > healthPerBar * 2)
        {
            currentHealthBar = 2;
            healthBarFills[1].fillAmount = 1;
            healthBarFills[0].fillAmount = 1;


        }
        else if(currentHealth < healthPerBar * 2 && currentHealth > healthPerBar)
        {
            currentHealthBar = 1;
            healthBarFills[2].fillAmount = 0;
            healthBarFills[0].fillAmount = 1;
        }
        else
        {
            currentHealthBar = 0;
            healthBarFills[2].fillAmount = 0;
            healthBarFills[1].fillAmount = 0;
        }
        hpText.text = currentHealth.ToString();
    }

    public void SetShieldBar(int currentShield, int maxShield)
    {
        shieldBarFill.fillAmount = (float)currentShield/(float)maxShield;
        shieldBarFader.fillAmount = shieldBarFill.fillAmount;
        spText.text = currentShield.ToString();
    }

    private IEnumerator AnimateBar(Image barFade, Image barFill, float fillAmount)
    {
        barFill.fillAmount = fillAmount;
        yield return new WaitForSeconds(0.4f);

        barFade.fillAmount = barFill.fillAmount;

        yield return null;
    }

    private float HealthRemap(int value, int from1, int to1, int from2, int to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
