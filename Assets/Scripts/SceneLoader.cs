using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // ---------- LOAD LEVELS ----------

    public void LoadGoma()
    {
        SceneManager.LoadScene("Goma_Level");
    }

    public void LoadGuri()
    {
        SceneManager.LoadScene("Guri_Level");
    }

    public void LoadRangi()
    {
        SceneManager.LoadScene("Rangi_Level");
    }

    public void LoadKiri()
    {
        SceneManager.LoadScene("Kiri_Level");
    }

    public void LoadYangi()
    {
        SceneManager.LoadScene("Yangi_Level");
    }

    // ---------- FINISH LEVELS ----------

    public void FinishGoma()
    {
        GameProgress.mapState = 1;
        SceneManager.LoadScene("BookScene");
    }

    public void FinishGuri()
    {
        GameProgress.mapState = 2;
        SceneManager.LoadScene("BookScene");
    }

    public void FinishRangi()
    {
        GameProgress.mapState = 3;
        SceneManager.LoadScene("BookScene");
    }

    public void FinishKiri()
    {
        GameProgress.mapState = 4;
        SceneManager.LoadScene("BookScene");
    }

    public void FinishYangi()
    {
        GameProgress.mapState = 5;

        // For now return to the book.
        // Later we'll replace this with the ending cutscene.
        SceneManager.LoadScene("BookScene");
    }
}