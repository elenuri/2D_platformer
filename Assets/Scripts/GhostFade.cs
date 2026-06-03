using UnityEngine;

public class GhostFade : MonoBehaviour
{
    public float lifetime = 0.3f;

    private SpriteRenderer sr;
    private Color startColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        float alpha = Mathf.Lerp(
            startColor.a,
            0f,
            1f - (lifetime / Mathf.Max(lifetime, 0.001f))
        );
    }
}