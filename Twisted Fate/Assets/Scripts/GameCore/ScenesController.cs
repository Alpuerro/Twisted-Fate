using UnityEngine;
using UnityEngine.SceneManagement;

/* Scenes index
 * Main     0
 * Menu     1 
 * Combat   2
 * Pause    3
 */

public class ScenesController : MonoBehaviour
{
    private static AsyncOperation asyncSceneLoad;

    public static void LoadScene(string name)
    {
        asyncSceneLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    public static void UnloadScene(string name)
    {
        asyncSceneLoad = SceneManager.UnloadSceneAsync(name, UnloadSceneOptions.None);
    }
}
