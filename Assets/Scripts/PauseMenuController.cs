using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Menu")]
    public GameObject pauseMenu;

    [Header("Buttons")]
    public GameObject backButton;

    public static string currentLevelScene = "";

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "BookScene")
        {
            currentLevelScene = sceneName;
        }
    }

    public void OpenPauseMenu()
    {
        HideButtons();

        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ShowButtons()
    {
        backButton.SetActive(true);
    }

    public void HideButtons()
    {
        backButton.SetActive(false);
    }

    // ---------- NAVIGATION ----------

    public void BackToCurrentLevel()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(currentLevelScene))
        {
            SceneManager.LoadScene(currentLevelScene);
        }
        else
        {
            Debug.LogWarning("No current level has been saved.");
        }
    }
}