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

    // Used when another scene wants to tell the book
    // which page to open.
    public static int requestedPage = -1;

    private int currentPage;
    private bool isFlipping = false;

    void Start()
    {
        turningPage.enabled = false;

        // If another scene specifically requested a page,
        // use that page.
        if (requestedPage >= 0)
        {
            currentPage = requestedPage;

            // Reset so the next normal visit behaves normally.
            requestedPage = -1;
        }
        else
        {
            // Normal game flow
            if (GameProgress.mapState == 0)
            {
                currentPage = 0; // Instructions
            }
            else
            {
                currentPage = 1; // Level Map
            }
        }

        ShowStaticPage(currentPage);
        ShowHotspots(currentPage);

        // Show Level Map only when on the map page
        if (levelMap != null)
            levelMap.SetActive(currentPage == 1);

        // Start map progression only when actually opening the map
        if (currentPage == 1 && mapProgression != null)
        {
            mapProgression.OnMapShown();
        }
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

    // Animation Event
    public void FinishFlip()
    {
        turningPage.enabled = false;

        currentPage++;

        ShowHotspots(currentPage);

        // Only show the Level Map on the map page
        if (levelMap != null)
            levelMap.SetActive(currentPage == 1);

        // If we arrived on the Level Map, start its progression
        if (currentPage == 1 && mapProgression != null)
        {
            mapProgression.OnMapShown();
        }

        pageAnimator.Play("Idle", 0, 0f);

        isFlipping = false;
    }
}