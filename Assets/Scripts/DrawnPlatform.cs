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

    public void Initialize(List<Vector2> points)
    {
        edgeCollider.points = points.ToArray();

        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}