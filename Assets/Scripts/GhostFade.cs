using UnityEngine;

public class GhostFade : MonoBehaviour
{
    public float lifetime = 0.3f;

    private SpriteRenderer sr;
    private Color startColor;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        startColor = sr.color;
        startColor.a = 0.6f; // Ghost transparency

        sr.color = startColor;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = timer / lifetime;

        Color c = startColor;
        c.a = Mathf.Lerp(startColor.a, 0f, t);

        sr.color = c;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}