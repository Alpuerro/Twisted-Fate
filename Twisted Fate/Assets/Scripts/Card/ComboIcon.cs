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
        _textBox.text = text;
        AnimateIcon();
    }

    private void AnimateIcon()
    {
        if (spawnVFX != null) spawnVFX.Play();

        Sequence sequence = DOTween.Sequence();
        Vector3 finalPosition = transform.position + Vector3.up * 100f;

        sequence.Append(transform.DOScale(Vector3.one, 0.2f));
        sequence.Join(transform.DOMove(finalPosition, 2.5f));
        sequence.Join(_canvasGroup.DOFade(0.0f, 1.0f).SetDelay(2));
        sequence.AppendCallback(() => Destroy(gameObject));

        sequence.Play();
    }
}
