using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ComboIcon : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textBox;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] ParticleSystem spawnVFX;

    public void CreateIcon(string text)
    {
        _textBox.text = "x"+text;
        AnimateIcon();
    }

    private void AnimateIcon()
    {
        if (spawnVFX != null) spawnVFX.Play();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_canvasGroup.DOFade(1.0f, 0.15f).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOScale(Vector3.one * 0.9f, 0.1f).SetEase(Ease.InSine));

        sequence.Play();
    }

    public void DestroyIcon()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_canvasGroup.DOFade(0.0f, 0.3f).SetEase(Ease.InOutSine).SetDelay(1.5f));
        sequence.AppendCallback(() => Destroy(gameObject));
        sequence.Play();
    }
}
