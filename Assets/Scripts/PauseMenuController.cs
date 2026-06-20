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

    public void OpenPauseMenu()
    {
        // Hide buttons before animation starts
        HideButtons();

        // Show menu
        pauseMenu.SetActive(true);

        // Pause game
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // Hide menu
        pauseMenu.SetActive(false);

        // Resume game
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

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BookScene");
    }

    public void GoToInstructions()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BookScene");
    }

    public void GoToLevelMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BookScene");
    }
}