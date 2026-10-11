using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SceneManager : MonoBehaviour
{
    private string endGameString = "Congratulations! You have completed the game!";

    [SerializeField] TMP_Text endGameText;
    [SerializeField] Animator endGameAnimator;

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

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        string results = BuildEndGameString();
        string text = string.IsNullOrEmpty(results) ? endGameString : endGameString + "\n" + results;
        Debug.Log("EndGame: " + text);

        if (endGameText == null)
            Debug.LogWarning("SceneManager: endGameText is not assigned.", this);
        else
            endGameText.text = text;

        if (endGameAnimator == null)
            Debug.LogWarning("SceneManager: endGameAnimator is not assigned.", this);
        else
            endGameAnimator.SetBool("Start", true);
    }

    private string BuildEndGameString()
    {
        GameState state = GameState.Instance;
        if (state == null)
        {
            Debug.LogWarning("SceneManager: no GameState in this scene, can't build the end game string.", this);
            return "";
        }

        List<string> results = new List<string>();
        if (state.SpikeFishCaught) results.Add(TextRepository.Results.BestAnger);
        if (state.UglyFishCaught) results.Add(TextRepository.Results.BestDisgust);
        if (state.ShrimpFishCaught) results.Add(TextRepository.Results.BestFear);
        if (state.IceFishCaught) results.Add(TextRepository.Results.BestHappiness);
        if (state.RedFishCaught) results.Add(TextRepository.Results.BestSadness);

        return string.Join("\n", results);
    }
}
