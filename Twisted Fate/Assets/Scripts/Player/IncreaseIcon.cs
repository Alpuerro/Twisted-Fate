using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IncreaseIcon : MonoBehaviour
{
    [SerializeField] Image icon;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;        
    }

    public void PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        transform.localPosition = startPosition;

        sequence.Append(icon.DOFade(1.0f, 0.2f));
        sequence.Join(transform.DOLocalMoveY(startPosition.y + 50f, 0.5f));
        sequence.Append(icon.DOFade(0.0f, 0.3f));

        sequence.Play();
    }
}
