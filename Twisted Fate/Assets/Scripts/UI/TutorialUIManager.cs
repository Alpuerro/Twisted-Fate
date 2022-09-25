using System.Collections;
using System.Collections.Generic;
using SceneNamesspace;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialPanels;
    [SerializeField] Button lastButton;
    [SerializeField] Button nextButton;

    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneTransition.instance != null) SceneTransition.instance.FadeOutTransition();
        ShowTutorial();
    }

    public void NextPanel()
    {
        index++;
        index = Mathf.Clamp(index, 0, tutorialPanels.Length);
        ShowTutorial();
    }

    public void LastPanel()
    {
        index--;
        index = Mathf.Clamp(index, 0, tutorialPanels.Length);
        ShowTutorial();
    }

    private void ShowTutorial()
    {
        if (index == 0) lastButton.gameObject.SetActive(false);
        else lastButton.gameObject.SetActive(true);
        if (index >= tutorialPanels.Length) nextButton.gameObject.SetActive(false);
        else nextButton.gameObject.SetActive(true);

        if (index >= tutorialPanels.Length)
        {
            SceneTransition.instance.FadeInTransition(SceneNames.Menu, SceneNames.Tutorial, false, true);
            return;
        }
        tutorialPanels[index].SetActive(true);

        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            if (i == index) continue;
            tutorialPanels[i].SetActive(false);
        }
    }
}
