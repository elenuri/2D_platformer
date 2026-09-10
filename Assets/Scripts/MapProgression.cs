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
        ShowCurrentMapState();

        switch (GameProgress.mapState)
        {
            case 0:
                if (gomaAnimator != null)
                {
                    gomaAnimator.gameObject.SetActive(true);
                    gomaAnimator.SetTrigger("Play");
                }
                break;

            case 1:
                if (pathToGuriAnimator != null)
                {
                    pathToGuriAnimator.gameObject.SetActive(true);
                    pathToGuriAnimator.SetTrigger("Play");
                }
                break;

            case 2:
                if (pathToRangiAnimator != null)
                {
                    pathToRangiAnimator.gameObject.SetActive(true);
                    pathToRangiAnimator.SetTrigger("Play");
                }
                break;

            case 3:
                if (pathToKiriAnimator != null)
                {
                    pathToKiriAnimator.gameObject.SetActive(true);
                    pathToKiriAnimator.SetTrigger("Play");
                }
                break;

            case 4:
                if (pathToYangiAnimator != null)
                {
                    pathToYangiAnimator.gameObject.SetActive(true);
                    pathToYangiAnimator.SetTrigger("Play");
                }
                break;

            case 5:
                break;
        }
    }

    public void ShowCurrentMapState()
    {
        // --------------------------------------------------
        // HIDE EVERYTHING FIRST
        // --------------------------------------------------

        if (guriPathFinished != null)
            guriPathFinished.SetActive(false);

        if (rangiPathFinished != null)
            rangiPathFinished.SetActive(false);

        if (kiriPathFinished != null)
            kiriPathFinished.SetActive(false);

        if (yangiPathFinished != null)
            yangiPathFinished.SetActive(false);

        if (gomaHotspot != null)
            gomaHotspot.SetActive(false);

        if (guriHotspot != null)
            guriHotspot.SetActive(false);

        if (rangiHotspot != null)
            rangiHotspot.SetActive(false);

        if (kiriHotspot != null)
            kiriHotspot.SetActive(false);

        if (yangiHotspot != null)
            yangiHotspot.SetActive(false);

        // --------------------------------------------------
        // NORMAL GAME PROGRESS
        // --------------------------------------------------

        switch (GameProgress.mapState)
        {
            case 0:
                break;

            case 1:
                gomaHotspot.SetActive(true);
                break;

            case 2:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);
                guriPathFinished.SetActive(true);
                break;

            case 3:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);
                rangiHotspot.SetActive(true);

                guriPathFinished.SetActive(true);
                rangiPathFinished.SetActive(true);
                break;

            case 4:
                gomaHotspot.SetActive(true);
                guriHotspot.SetActive(true);
                rangiHotspot.SetActive(true);
                kiriHotspot.SetActive(true);

                guriPathFinished.SetActive(true);
                rangiPathFinished.SetActive(true);
                kiriPathFinished.SetActive(true);
                break;

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

        // --------------------------------------------------
        // CURRENT LEVEL OVERRIDE
        // --------------------------------------------------

        string currentLevel = PauseMenuController.currentLevelScene;

        // Goma is the first level.
        // mapState is still 0 while playing it,
        // so we need to show Goma manually.
        if (currentLevel == "Goma_Level")
        {
            if (gomaHotspot != null)
                gomaHotspot.SetActive(true);
        }

        else if (currentLevel == "Guri_Level")
        {
            if (gomaHotspot != null)
                gomaHotspot.SetActive(true);

            if (guriHotspot != null)
                guriHotspot.SetActive(true);

            if (guriPathFinished != null)
                guriPathFinished.SetActive(true);
        }

        else if (currentLevel == "Rangi_Level")
        {
            if (gomaHotspot != null)
                gomaHotspot.SetActive(true);

            if (guriHotspot != null)
                guriHotspot.SetActive(true);

            if (rangiHotspot != null)
                rangiHotspot.SetActive(true);

            if (guriPathFinished != null)
                guriPathFinished.SetActive(true);

            if (rangiPathFinished != null)
                rangiPathFinished.SetActive(true);
        }

        else if (currentLevel == "Kiri_Level")
        {
            if (gomaHotspot != null)
                gomaHotspot.SetActive(true);

            if (guriHotspot != null)
                guriHotspot.SetActive(true);

            if (rangiHotspot != null)
                rangiHotspot.SetActive(true);

            if (kiriHotspot != null)
                kiriHotspot.SetActive(true);

            if (guriPathFinished != null)
                guriPathFinished.SetActive(true);

            if (rangiPathFinished != null)
                rangiPathFinished.SetActive(true);

            if (kiriPathFinished != null)
                kiriPathFinished.SetActive(true);
        }

        else if (currentLevel == "Yangi_Level")
        {
            if (gomaHotspot != null)
                gomaHotspot.SetActive(true);

            if (guriHotspot != null)
                guriHotspot.SetActive(true);

            if (rangiHotspot != null)
                rangiHotspot.SetActive(true);

            if (kiriHotspot != null)
                kiriHotspot.SetActive(true);

            if (yangiHotspot != null)
                yangiHotspot.SetActive(true);

            if (guriPathFinished != null)
                guriPathFinished.SetActive(true);

            if (rangiPathFinished != null)
                rangiPathFinished.SetActive(true);

            if (kiriPathFinished != null)
                kiriPathFinished.SetActive(true);

            if (yangiPathFinished != null)
                yangiPathFinished.SetActive(true);
        }
    }

    public void PlayGuriUnlock()
    {
        if (guriAnimator != null)
            guriAnimator.SetTrigger("Play");
    }

    public void PlayRangiUnlock()
    {
        if (rangiAnimator != null)
            rangiAnimator.SetTrigger("Play");
    }

    public void PlayKiriUnlock()
    {
        if (kiriAnimator != null)
            kiriAnimator.SetTrigger("Play");
    }

    public void PlayYangiUnlock()
    {
        if (yangiAnimator != null)
            yangiAnimator.SetTrigger("Play");
    }
}