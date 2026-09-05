using UnityEngine;

public class HotspotFeedback : MonoBehaviour
{
    public Transform icon;

    public float hoverScale = 1.08f;
    public float clickScale = 0.95f;
    public float speed = 10f;

    private Vector3 normalScale;
    private Vector3 targetScale;

    void Start()
    {
        normalScale = icon.localScale;
        targetScale = normalScale;
    }

    void Update()
    {
        icon.localScale = Vector3.Lerp(
            icon.localScale,
            targetScale,
            Time.deltaTime * speed);
    }

    void OnMouseEnter()
    {
        targetScale = normalScale * hoverScale;
    }

    void OnMouseExit()
    {
        targetScale = normalScale;
    }

    void OnMouseDown()
    {
        targetScale = normalScale * clickScale;
    }

    void OnMouseUp()
    {
        targetScale = normalScale * hoverScale;
    }
}