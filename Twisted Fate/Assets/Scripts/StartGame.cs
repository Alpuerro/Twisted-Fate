using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
