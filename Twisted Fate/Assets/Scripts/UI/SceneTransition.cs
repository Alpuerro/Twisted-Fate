using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else 
        {
            Destroy(this);
        }
    }

    public void FadeInTransition(int sceneToLoad)
    {
        Sequence fadeIn = DOTween.Sequence();

        fadeIn.Append(transform.DOLocalMoveX(0.0f, 0.8f).SetEase(Ease.InOutSine));
        fadeIn.AppendCallback(() =>
        {
            ScenesController.LoadScene((SceneNamesspace.SceneNames)sceneToLoad);
        });

        fadeIn.Play();
    }

    public void FadeOutTransition()
    {
        Sequence fadeOut = DOTween.Sequence();

        fadeOut.Append(transform.DOLocalMoveX(-Screen.width, 0.8f).SetEase(Ease.InOutSine));

        fadeOut.Play();
    }
}
