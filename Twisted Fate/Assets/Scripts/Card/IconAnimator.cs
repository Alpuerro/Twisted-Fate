using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IconAnimator : MonoBehaviour
{
    [SerializeField] int cardType;
    [SerializeField] Image character;
    [SerializeField] Image icon;
    // Start is called before the first frame update
    void Start()
    {
        //PlayIconAnimator();
    }

    public void PlayIconAnimator()
    {
        switch (cardType)
        {
            case 0:
                PlayRainbowAnimation();
                break;
            case 1:
                PlayHeartAnimation();
                break;
            case 2:
                PlayShieldAnimation();
                break;
            case 3:
                PlayDrawAnimation();
                break;
            case 4:
                PlayStunAnimation();
                break;
            case 5:
                PlayDamageUpAnimation();
                break;
        }
    }


    public void StopAnimations()
    {
    }
    private void PlayDamageUpAnimation()
    {
        Vector3 iconStartPosition = icon.transform.localPosition;
        Sequence animation = DOTween.Sequence();
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, -6), 0.3f).SetEase(Ease.OutSine).SetDelay(0.5f));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 5), 0.5f).SetEase(Ease.InOutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, -4), 0.4f).SetEase(Ease.InOutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 3), 0.3f).SetEase(Ease.InOutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, -2), 0.25f).SetEase(Ease.InOutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 1), 0.1f).SetEase(Ease.InOutSine));
        animation.SetLoops(-1);
        animation.Play();

        Sequence iconAnimation = DOTween.Sequence();
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y + 1.5f, 0.5f).SetEase(Ease.OutSine).SetDelay(0.5f));
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y, 0.5f).SetEase(Ease.InSine));

        iconAnimation.SetLoops(-1);
        iconAnimation.Play();
    }

    private void PlayStunAnimation()
    {
        Vector3 iconStartPosition = icon.transform.localPosition;
        Sequence animation = DOTween.Sequence();
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, -3), 0.05f).SetDelay(1f));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 35), 0.25f).SetEase(Ease.InSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 45), 0.06f).SetEase(Ease.OutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.InSine));
        animation.SetLoops(-1);
        animation.Play();

        Sequence iconAnimation = DOTween.Sequence();
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y + 3f, 0.5f).SetEase(Ease.OutSine).SetDelay(0.5f));
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y, 0.5f).SetEase(Ease.InSine));

        iconAnimation.SetLoops(-1);
        iconAnimation.Play();
    }

    private void PlayDrawAnimation()
    {
        Vector3 iconStartPosition = icon.transform.localPosition;
        Sequence animation = DOTween.Sequence();
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, -5), 0.5f).SetEase(Ease.OutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 5), 1f).SetEase(Ease.InOutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InSine));
        animation.SetLoops(-1);
        animation.Play();

        Sequence iconAnimation = DOTween.Sequence();
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y + 1.5f, 0.5f).SetEase(Ease.OutSine).SetDelay(0.5f));
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y, 0.5f).SetEase(Ease.InSine));

        iconAnimation.SetLoops(-1);
        iconAnimation.Play();
    }

    private void PlayShieldAnimation()
    {
        Vector3 iconStartPosition = icon.transform.localPosition;
        Sequence animation = DOTween.Sequence();
        animation.Append(character.transform.DOScaleY(0.9f, 0.4f).SetEase(Ease.OutSine));
        animation.Append(character.transform.DOScaleY(1.0f, 0.4f).SetEase(Ease.OutSine).SetDelay(1.0f));
        animation.SetLoops(-1);
        animation.Play();

        Sequence iconAnimation = DOTween.Sequence();
        iconAnimation.Append(icon.transform.DOLocalMoveY(iconStartPosition.y + 20f, 1.8f).SetEase(Ease.OutSine));
        iconAnimation.Join(icon.DOFade(1.0f, 1.5f).SetEase(Ease.OutSine));

        iconAnimation.SetLoops(-1);
        iconAnimation.Play();
    }

    private void PlayHeartAnimation()
    {
        Sequence animation = DOTween.Sequence();
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 1), 0.1f).SetEase(Ease.OutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, -10), 1f).SetEase(Ease.InOutSine));
        animation.Append(character.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InSine));
        animation.SetLoops(-1);
        animation.Play();

        Sequence iconAnimation = DOTween.Sequence();
        iconAnimation.Append(icon.transform.DOScale(Vector3.one * 1.1f, 0.25f).SetEase(Ease.OutSine));
        iconAnimation.Append(icon.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutSine));

        iconAnimation.SetLoops(-1);
        iconAnimation.Play();
    }

    private void PlayRainbowAnimation()
    {
        throw new NotImplementedException();
    }
}
