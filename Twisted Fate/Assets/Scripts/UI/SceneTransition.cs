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

    /// <param name="nextScene"></param>
    /// <param name="currentScene"></param>
    /// <param name="load">True if the next scene is to be loaded.</param>
    /// <param name="unload">True if the current scene is to be unloaded.</param>
    public void FadeInTransition(SceneNames nextScene, SceneNames currentScene, bool load, bool unload)
    {
        Sequence fadeIn = DOTween.Sequence();

        fadeIn.Append(background.DOLocalMoveX(0.0f, 0.8f).SetEase(Ease.InOutSine));
        if (load)
            fadeIn.AppendCallback(() => ScenesController.LoadScene(nextScene));
        if (!load)
            FadeOutTransition();
        if (unload)
        {
            FadeOutTransition();
            fadeIn.AppendCallback(() => ScenesController.UnloadScene(currentScene));
        }

        fadeIn.Play();
    }

    public void FadeOutTransition()
    {
        Sequence fadeOut = DOTween.Sequence();
        fadeOut.Append(background.DOLocalMoveX(-Screen.width * 2, 0.8f).SetEase(Ease.InOutSine));
        fadeOut.Play();
    }
}
