using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SceneNamesspace;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup creditsPanel;
    Vector3 startPositionCredits;

    private void Start()
    {
        if (SceneTransition.instance != null) SceneTransition.instance.FadeOutTransition();
        startPositionCredits = creditsPanel.transform.localPosition;
        SharedDataManager.SetDataByKey("round", 1);
    }

    public void StartGame()
    {
        SceneTransition.instance.FadeInTransition(SceneNames.Combat, SceneNames.Menu, true, true);
    }
    public void StartTutorial()
    {
        SceneTransition.instance.FadeInTransition(SceneNames.Tutorial, SceneNames.Menu, true, false);
    }


    public void ShowCredits()
    {
        Sequence showCredits = DOTween.Sequence();
        creditsPanel.interactable = true;
        creditsPanel.blocksRaycasts = true;

        showCredits.Append(creditsPanel.DOFade(1.0f, 1.0f).SetEase(Ease.InOutSine).SetDelay(0.3f));
        showCredits.Join(creditsPanel.transform.DOLocalMove(Vector3.zero, 0.7f).SetEase(Ease.InOutSine));

        showCredits.Play();
    }

    public void CloseCredits()
    {
        Sequence closeCredits = DOTween.Sequence();
        creditsPanel.interactable = false;
        creditsPanel.blocksRaycasts = false;

        closeCredits.Append(creditsPanel.DOFade(0.0f, 1.0f).SetEase(Ease.InOutSine));
        closeCredits.Join(creditsPanel.transform.DOLocalMove(startPositionCredits, 0.7f).SetEase(Ease.InOutSine));

        closeCredits.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
