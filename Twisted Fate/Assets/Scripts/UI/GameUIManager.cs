using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup pausePanel;

    public void PauseGame()
    {
        //TODO pausar el juego de verdad
        pausePanel.blocksRaycasts = true;
        pausePanel.interactable = true;

        pausePanel.DOFade(1.0f, 0.5f).SetEase(Ease.InOutSine);
    }

    public void RestartGame()
    {
        //TODO restaurar el juego
        pausePanel.blocksRaycasts = false;
        pausePanel.interactable = false;

        pausePanel.DOFade(0.0f, 0.5f).SetEase(Ease.InOutSine);
    }
}
