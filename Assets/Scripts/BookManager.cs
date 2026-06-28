using UnityEngine;

public class BookManager : MonoBehaviour
{
    [Header("Page Textures")]
    public Texture2D[] pages;

    [Header("Renderers")]
    public MeshRenderer staticPage;
    public MeshRenderer turningPage;

    [Header("Animator")]
    public Animator pageAnimator;

    private int currentPage = 0;
    private bool isFlipping = false;

    void Start()
    {
        turningPage.enabled = false;
        ShowStaticPage(currentPage);
    }

    void ShowStaticPage(int index)
    {
        Material[] mats = staticPage.materials;
        mats[0].SetTexture("_BaseMap", pages[index]);
        staticPage.materials = mats;
    }

    void ShowTurningPage(int index)
    {
        Material[] mats = turningPage.materials;
        mats[0].SetTexture("_BaseMap", pages[index]);
        turningPage.materials = mats;
    }

    public void NextPage()
    {
        if (isFlipping)
            return;

        if (currentPage >= pages.Length - 1)
            return;

        isFlipping = true;

        // Turning page shows current page
        ShowTurningPage(currentPage);

        // Static page immediately becomes next page
        ShowStaticPage(currentPage + 1);

        turningPage.enabled = true;

        pageAnimator.ResetTrigger("Flip");
        pageAnimator.SetTrigger("Flip");
    }

    // Called by the LAST animation event
    public void FinishFlip()
    {
        turningPage.enabled = false;

        currentPage++;

        // Force animator back to Idle immediately
        pageAnimator.Play("Idle", 0, 0f);

        isFlipping = false;
    }
}