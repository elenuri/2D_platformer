using UnityEngine;

public class BookManager : MonoBehaviour
{
    public int currentPage = 0;

    public GameObject instructionsPage;
    public GameObject levelMapPage;
    public GameObject gomaPage;

    public Animator pageAnimator;

    void Start()
    {
       // ShowPage(currentPage);
    }

    public void ShowPage(int page)
    {
        instructionsPage.SetActive(false);
        levelMapPage.SetActive(false);
        gomaPage.SetActive(false);

        switch (page)
        {
            case 0:
                instructionsPage.SetActive(true);
                break;

            case 1:
                levelMapPage.SetActive(true);
                break;

            case 2:
                gomaPage.SetActive(true);
                break;
        }
    }

    public void NextPage()
    {
        pageAnimator.SetTrigger("Flip");
    }

    public void PreviousPage()
    {
        currentPage--;

        if (currentPage < 0)
            currentPage = 0;

        ShowPage(currentPage);
    }

    // Called later from Animation Event
    public void SwapToNextPage()
    {
        currentPage++;

        if (currentPage > 2)
            currentPage = 2;

        ShowPage(currentPage);
    }
}