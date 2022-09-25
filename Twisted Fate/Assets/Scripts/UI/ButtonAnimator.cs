using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimator : MonoBehaviour
{
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Animate);
    }

    public void Animate()
    {
        transform.DOScale(0.95f, 0.2f).SetEase(Ease.InSine);
        transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutSine).SetDelay(0.2f);
    }
}
