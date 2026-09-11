using System.Collections.Generic;
using UnityEngine;

public class DrawnPlatform : MonoBehaviour
{
    [Header("Lifetime")]
    public float lifeTime = 5f;

    private EdgeCollider2D edgeCollider;
    private LineRenderer lineRenderer;

    void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(
        List<Vector2> points,
        Material brushMaterial,
        Color brushColor,
        float brushWidth
    )
    {
        // Apply this level's brush material
        lineRenderer.material = brushMaterial;

        // Apply this level's brush color
        lineRenderer.startColor = brushColor;
        lineRenderer.endColor = brushColor;

        // Apply this level's brush width
        lineRenderer.startWidth = brushWidth;
        lineRenderer.endWidth = brushWidth;

        // Set up the physics collider
        edgeCollider.points = points.ToArray();

        // Set up the visual line
        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}