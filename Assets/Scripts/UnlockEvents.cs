using UnityEngine;

public class UnlockEvents : MonoBehaviour
{
    [Header("Objects")]
    public GameObject hotspot;
    public GameObject placeholder;
    public GameObject finishedPath;
    public GameObject pathAnimation;

    [Header("References")]
    public MapProgression mapProgression;

    // Called at the end of every character unlock animation
    public void EnableHotspot()
    {
        // Show the permanent finished path
        if (finishedPath != null)
            finishedPath.SetActive(true);

        // Hide the animated path
        if (pathAnimation != null)
            pathAnimation.SetActive(false);

        // Hide the placeholder
        if (placeholder != null)
            placeholder.SetActive(false);

        // Enable the clickable hotspot
        if (hotspot != null)
            hotspot.SetActive(true);

        // Turn off this unlock animation object
        gameObject.SetActive(false);
    }

    // Called by the end of the path animations
    public void PlayGuriUnlock()
    {
        if (mapProgression != null)
            mapProgression.PlayGuriUnlock();
    }

    public void PlayRangiUnlock()
    {
        if (mapProgression != null)
            mapProgression.PlayRangiUnlock();
    }

    public void PlayKiriUnlock()
    {
        if (mapProgression != null)
            mapProgression.PlayKiriUnlock();
    }

    public void PlayYangiUnlock()
    {
        if (mapProgression != null)
            mapProgression.PlayYangiUnlock();
    }
}