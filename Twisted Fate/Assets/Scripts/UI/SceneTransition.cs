using UnityEngine;
using DG.Tweening;
using SceneNamesspace;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    [SerializeField] RectTransform background;

    SceneNames lastSceneLoaded;

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

    public void FadeInTransition(SceneNames sceneName, SceneNames lastSceneNames)
    {
        Sequence fadeIn = DOTween.Sequence();
        lastSceneLoaded = lastSceneNames;

        fadeIn.Append(background.DOLocalMoveX(0.0f, 0.8f).SetEase(Ease.InOutSine));
        fadeIn.AppendCallback(() =>
        {
            ScenesController.LoadScene(sceneName);
        });

        fadeIn.Play();
    }

    public void FadeOutTransition()
    {
        if (lastSceneLoaded != null)
        {
            Sequence fadeOut = DOTween.Sequence();

            fadeOut.Append(background.DOLocalMoveX(-Screen.width * 2, 0.8f).SetEase(Ease.InOutSine));
            fadeOut.AppendCallback(() =>
            {
                ScenesController.UnloadScene(lastSceneLoaded);
            });

            fadeOut.Play();
        }
    }
}
