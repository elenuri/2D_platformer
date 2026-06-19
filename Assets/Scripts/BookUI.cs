using UnityEngine;
using UnityEngine.SceneManagement;

public class BookUI : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject openBookAnimation;
    public GameObject gomaPage;

    public void OpenBook()
    {
        startScreen.SetActive(false);
        openBookAnimation.SetActive(true);
    }

    public void ShowGomaPage()
    {
        openBookAnimation.SetActive(false);
        gomaPage.SetActive(true);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Goma_Level");
    }
}