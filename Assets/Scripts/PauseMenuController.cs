using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Menu")]
    public GameObject pauseMenu;

    [Header("Buttons")]
    public GameObject backButton;
    public GameObject homeButton;
    public GameObject instructionsButton;
    public GameObject levelMapButton;

    // Remembers the level the player came from
    public static string currentLevelScene = "";

    private void Start()
    {
        // If this controller is in a level scene,
        // automatically remember that scene.
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

    // Called by Animation Event
    public void ShowButtons()
    {
        backButton.SetActive(true);
        homeButton.SetActive(true);
        instructionsButton.SetActive(true);
        levelMapButton.SetActive(true);
    }

    public void HideButtons()
    {
        backButton.SetActive(false);
        homeButton.SetActive(false);
        instructionsButton.SetActive(false);
        levelMapButton.SetActive(false);
    }

    // ---------- NAVIGATION ----------

    public void GoHome()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("BookScene");
    }

    public void GoToInstructions()
    {
        Time.timeScale = 1f;

        BookManager.requestedPage = 0;

        SceneManager.LoadScene("BookScene");
    }

    public void GoToLevelMap()
    {
        Time.timeScale = 1f;

        BookManager.requestedPage = 1;

        SceneManager.LoadScene("BookScene");
    }

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