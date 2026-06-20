using UnityEngine;
using UnityEngine.SceneManagement;

public class BookUI : MonoBehaviour
{
    [Header("Pages")]
    public GameObject startScreen;
    public GameObject openBookAnimation;
    public GameObject instructionsPage;
    public GameObject levelMapPage;
    public GameObject gomaPage;

    // Called when player clicks the sketchbook on the desk
    public void OpenBook()
    {
        startScreen.SetActive(false);
        openBookAnimation.SetActive(true);
    }

    // Called by Animation Event at the end of the book-opening animation
    public void ShowInstructionsPage()
    {
        openBookAnimation.SetActive(false);
        instructionsPage.SetActive(true);
    }

    // Called by Continue button on InstructionsPage
    public void ShowLevelMap()
    {
        instructionsPage.SetActive(false);
        levelMapPage.SetActive(true);
    }

    // Called by Goma button on LevelMapPage
    public void ShowGomaPage()
    {
        levelMapPage.SetActive(false);
        gomaPage.SetActive(true);
    }

    // Called by Start button on GomaPage
    public void StartLevel()
    {
        SceneManager.LoadScene("Goma_Level");
    }

    // Optional Home function for future use
    public void ShowHome()
    {
        instructionsPage.SetActive(false);
        levelMapPage.SetActive(false);
        gomaPage.SetActive(false);
        openBookAnimation.SetActive(false);

        startScreen.SetActive(true);
    }
}