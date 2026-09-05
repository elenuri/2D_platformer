using UnityEngine;

public class MapProgression : MonoBehaviour
{
    [Header("Unlock Animators")]
    public Animator gomaAnimator;
    public Animator pathToGuriAnimator;
    public Animator guriAnimator;

    public Animator pathToRangiAnimator;
    public Animator rangiAnimator;

    public Animator pathToKiriAnimator;
    public Animator kiriAnimator;

    public Animator pathToYangiAnimator;
    public Animator yangiAnimator;

    [Header("Hotspots")]
    public GameObject gomaHotspot;
    public GameObject guriHotspot;
    public GameObject rangiHotspot;
    public GameObject kiriHotspot;
    public GameObject yangiHotspot;

    [Header("Finished Paths")]
    public GameObject guriPathFinished;
    public GameObject rangiPathFinished;
    public GameObject kiriPathFinished;
    public GameObject yangiPathFinished;

    public void OnMapShown()
    {
        // Hide all finished paths by default
        guriPathFinished.SetActive(false);
        rangiPathFinished.SetActive(false);
        kiriPathFinished.SetActive(false);
        yangiPathFinished.SetActive(false);

        switch (GameProgress.mapState)
        {
            // ----------------------------------
            // First visit
            // ----------------------------------
            case 0:
                gomaAnimator.SetTrigger("Play");
                break;

            // ----------------------------------
            // Goma completed
            // ----------------------------------
            case 1:
                gomaHotspot.SetActive(true);

                pathToGuriAnimator.gameObject.SetActive(true);
                pathToGuriAnimator.SetTrigger("Play");
                break;

            // ----------------------------------
            // Guri completed
            // ----------------------------------
            case 2:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);

                guriPathFinished.SetActive(true);

                pathToRangiAnimator.gameObject.SetActive(true);
                pathToRangiAnimator.SetTrigger("Play");
                break;

            // ----------------------------------
            // Rangi completed
            // ----------------------------------
            case 3:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);
                rangiHotspot.SetActive(true);

                guriPathFinished.SetActive(true);
                rangiPathFinished.SetActive(true);

                pathToKiriAnimator.gameObject.SetActive(true);
                pathToKiriAnimator.SetTrigger("Play");
                break;

            // ----------------------------------
            // Kiri completed
            // ----------------------------------
            case 4:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);
                rangiHotspot.SetActive(true);
                kiriHotspot.SetActive(true);

                guriPathFinished.SetActive(true);
                rangiPathFinished.SetActive(true);
                kiriPathFinished.SetActive(true);

                pathToYangiAnimator.gameObject.SetActive(true);
                pathToYangiAnimator.SetTrigger("Play");
                break;

            // ----------------------------------
            // Game finished
            // ----------------------------------
            case 5:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);
                rangiHotspot.SetActive(true);
                kiriHotspot.SetActive(true);
                yangiHotspot.SetActive(true);

                guriPathFinished.SetActive(true);
                rangiPathFinished.SetActive(true);
                kiriPathFinished.SetActive(true);
                yangiPathFinished.SetActive(true);
                break;
        }
    }

    public void PlayGuriUnlock()
    {
        guriAnimator.SetTrigger("Play");
    }

    public void PlayRangiUnlock()
    {
        rangiAnimator.SetTrigger("Play");
    }

    public void PlayKiriUnlock()
    {
        kiriAnimator.SetTrigger("Play");
    }

    public void PlayYangiUnlock()
    {
        yangiAnimator.SetTrigger("Play");
    }
}