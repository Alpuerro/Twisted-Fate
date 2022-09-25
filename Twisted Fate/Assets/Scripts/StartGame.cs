using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private void Start()
    {
        SharedDataManager.SetDataByKey("score", 0);
        SharedDataManager.SetDataByKey("round", 1);
    }
}
