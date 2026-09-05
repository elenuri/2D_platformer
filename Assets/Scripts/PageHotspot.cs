using UnityEngine;
using UnityEngine.Events;

public class PageHotspot : MonoBehaviour
{
    public UnityEvent onClick;

    private void OnMouseDown()
    {
        onClick.Invoke();
    }
}