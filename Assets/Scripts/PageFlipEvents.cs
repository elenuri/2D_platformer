using UnityEngine;

public class PageFlipEvents : MonoBehaviour
{
    public BookManager bookManager;

    // Called by the LAST animation event only
    public void FinishFlip()
    {
        if (bookManager != null)
        {
            bookManager.FinishFlip();
        }
    }
}