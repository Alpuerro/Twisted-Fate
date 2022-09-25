using SceneNamesspace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    private static AsyncOperation asyncSceneLoad;

    public static void LoadScene(SceneNames name)
    {
        asyncSceneLoad = SceneManager.LoadSceneAsync((int)name, LoadSceneMode.Additive);
    }

    public static void UnloadScene(SceneNames name)
    {
        asyncSceneLoad = SceneManager.UnloadSceneAsync((int)name, UnloadSceneOptions.None);
    }
}

namespace SceneNamesspace
{
    public enum SceneNames
    {
        Main,
        Menu,
        Combat,
        Tutorial,
        Pause
    }
}