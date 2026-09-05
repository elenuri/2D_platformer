using UnityEngine;

public class BookManager : MonoBehaviour
{
    [Header("Page Textures")]
    public Texture2D[] pages;

    [Header("Page Renderers")]
    public MeshRenderer staticPage;
    public MeshRenderer turningPage;

    [Header("Animation")]
    public Animator pageAnimator;

    [Header("Hotspot Groups")]
    public GameObject[] hotspotGroups;

    [Header("Map")]
    public MapProgression mapProgression;
    public GameObject levelMap;

    public static int requestedPage = -1;

    // Kept for compatibility with PauseMenuController
    public static bool openedMapFromPause = false;

    private int currentPage;
    private bool isFlipping = false;

    void Start()
    {
        turningPage.enabled = false;

        bool openedSpecificPage = requestedPage >= 0;

        if (requestedPage >= 0)
        {
            currentPage = requestedPage;
            requestedPage = -1;
        }
        else
        {
            if (GameProgress.mapState == 0)
            {
                currentPage = 0;
            }
            else
            {
                currentPage = 1;
            }
        }

        ShowStaticPage(currentPage);
        ShowHotspots(currentPage);

        if (levelMap != null)
            levelMap.SetActive(currentPage == 1);

        if (currentPage == 1 && mapProgression != null)
        {
            if (openedSpecificPage || openedMapFromPause)
            {
                mapProgression.ShowCurrentMapState();
            }
            else
            {
                mapProgression.OnMapShown();
            }
        }

        openedMapFromPause = false;
    }

    void ShowStaticPage(int pageIndex)
    {
        Material[] mats = staticPage.materials;
        mats[0].SetTexture("_BaseMap", pages[pageIndex]);
        staticPage.materials = mats;
    }

    void ShowTurningPage(int pageIndex)
    {
        Material[] mats = turningPage.materials;
        mats[0].SetTexture("_BaseMap", pages[pageIndex]);
        turningPage.materials = mats;
    }

    void ShowHotspots(int pageIndex)
    {
        for (int i = 0; i < hotspotGroups.Length; i++)
        {
            hotspotGroups[i].SetActive(i == pageIndex);
        }
    }

    public void NextPage()
    {
        if (isFlipping)
            return;

        if (currentPage >= pages.Length - 1)
            return;

        isFlipping = true;

        if (currentPage < hotspotGroups.Length)
            hotspotGroups[currentPage].SetActive(false);

        ShowTurningPage(currentPage);
        ShowStaticPage(currentPage + 1);

        turningPage.enabled = true;

        pageAnimator.ResetTrigger("Flip");
        pageAnimator.SetTrigger("Flip");
    }

    public void FinishFlip()
    {
        turningPage.enabled = false;

        currentPage++;

        ShowHotspots(currentPage);

        if (levelMap != null)
            levelMap.SetActive(currentPage == 1);

        if (currentPage == 1 && mapProgression != null)
        {
            mapProgression.OnMapShown();
        }

        pageAnimator.Play("Idle", 0, 0f);

        isFlipping = false;
    }
}