using System.Collections.Generic;
using UnityEngine;

public class DrawnPlatform : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    private LineRenderer lineRenderer;

    void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Initialize(List<Vector2> points)
    {
        // Set collider
        edgeCollider.points = points.ToArray();

        // Set visual line
        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}