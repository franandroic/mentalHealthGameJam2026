using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // poziva se s Buttona (OnClick) - loada iducu scenu po redoslijedu iz Build Profiles / Build Settings
    // klasa se zove isto kao Unityjev SceneManager, zato se on poziva s punim imenom UnityEngine.SceneManagement.SceneManager
    public void LoadNextScene()
    {
        int nextIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Nema iduce scene u Build Settings, trenutna scena je zadnja");
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextIndex);
    }
}
