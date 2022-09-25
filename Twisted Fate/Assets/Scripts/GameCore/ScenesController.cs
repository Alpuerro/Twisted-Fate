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

    public static void ReloadScene(SceneNames name)
    {
        SceneManager.UnloadSceneAsync((int)name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.LoadScene((int)name, LoadSceneMode.Additive);
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