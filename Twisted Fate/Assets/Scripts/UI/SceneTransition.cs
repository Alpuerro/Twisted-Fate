using UnityEngine;
using DG.Tweening;
using SceneNamesspace;

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

    public void FadeInTransition(SceneNames sceneName)
    {
        Sequence fadeIn = DOTween.Sequence();

        fadeIn.Append(transform.DOLocalMoveX(0.0f, 0.8f).SetEase(Ease.InOutSine));
        fadeIn.AppendCallback(() =>
        {
            ScenesController.LoadScene(sceneName);
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
