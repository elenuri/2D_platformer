using System.Collections.Generic;
using UnityEngine;

public class DrawingSystem : MonoBehaviour
{
    [Header("Line Settings")]
    public LineRenderer lineRenderer;          // Visual line component
    public float minPointDistance = 0.1f;      // Minimum distance between points

    [Header("Shape Detection")]
    public float circleThreshold = 0.5f;       // How close start/end must be to count as "closed"
    public float lineTolerance = 0.2f;         // How strict line detection is
    public float circleTolerance = 0.3f;       // How strict circle detection is

    private List<Vector2> points = new List<Vector2>(); // Stores drawn points
    private Camera cam;
    private bool isDrawing = false;

    void Start()
    {
        cam = Camera.main;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // --- START DRAWING ---
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        // --- CONTINUE DRAWING ---
        if (Input.GetMouseButton(0) && isDrawing)
        {
            UpdateDrawing();
        }

        // --- END DRAWING ---
        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            EndDrawing();
        }
    }

    void StartDrawing()
    {
        isDrawing = true;

        points.Clear();
        lineRenderer.positionCount = 0;
    }
    void UpdateDrawing()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Only add point if far enough from last one
        if (points.Count == 0 || Vector2.Distance(points[points.Count - 1], mousePos) > minPointDistance)
        {
            points.Add(mousePos);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, mousePos);
        }
    }

    void EndDrawing()
    {
        isDrawing = false;

        // Not enough points → ignore
        if (points.Count < 2)
        {
            ClearLine();
            return;
        }

        // Find where the shape first closes
        int closeIndex = -1;

        for (int i = 5; i < points.Count; i++) // skip very early points
        {
            float dist = Vector2.Distance(points[0], points[i]);

            if (dist < circleThreshold)
            {
                closeIndex = i;
                break;
            }
        }
        bool isClosed = closeIndex != -1;
        List<Vector2> shapePoints = points;

        // --- SHAPE CLASSIFICATION ---
        if (isClosed && IsRoughCircle(points))
        {
            HandleCircle();
        }
        else if (IsMostlyStraightLine(points))
        {
            HandleLine();
        }
        else
        {
            Debug.Log("Unrecognized shape");
        }

        // Clear drawing after short delay (so you can see result if needed)
        Invoke(nameof(ClearLine), 0.1f);
    }

    // =========================
    // SHAPE DETECTION FUNCTIONS
    // =========================

    // Check if points form a mostly straight line
    bool IsMostlyStraightLine(List<Vector2> pts)
    {
        if (pts.Count < 2) return false;

        Vector2 start = pts[0];
        Vector2 end = pts[pts.Count - 1];
        Vector2 direction = (end - start).normalized;

        for (int i = 1; i < pts.Count - 1; i++)
        {
            Vector2 pointDir = (pts[i] - start).normalized;

            float dot = Vector2.Dot(direction, pointDir);

            // If point deviates too much → not a straight line
            if (dot < 1 - lineTolerance)
            {
                return false;
            }
        }

        return true;
    }

    // Check if points form a rough circle
    bool IsRoughCircle(List<Vector2> pts)
{
    if (pts.Count < 8) return false;

    // --- Get bounding box ---
    float minX = pts[0].x;
    float maxX = pts[0].x;
    float minY = pts[0].y;
    float maxY = pts[0].y;

    foreach (var p in pts)
    {
        if (p.x < minX) minX = p.x;
        if (p.x > maxX) maxX = p.x;
        if (p.y < minY) minY = p.y;
        if (p.y > maxY) maxY = p.y;
    }

    float width = maxX - minX;
    float height = maxY - minY;

    if (height == 0) return false;

    float ratio = width / height;

    // Accept slightly stretched shapes
    if (ratio < 0.3f || ratio > 3f)
    {
        return false;
    }

    // --- Check "roundness" ---
    float pathLength = 0f;
    for (int i = 1; i < pts.Count; i++)
    {
        pathLength += Vector2.Distance(pts[i - 1], pts[i]);
    }

    float straightDistance = Vector2.Distance(pts[0], pts[pts.Count - 1]);

    if (pathLength < straightDistance * 1.5f)
    {
        return false;
    }

    return true;
}

    // =========================
    // SHAPE HANDLERS
    // =========================

    void HandleLine()
    {
        Debug.Log("Line detected");

        // TODO: spawn platform here later
    }

    void HandleCircle()
    {
        Debug.Log("Circle detected");

        // TODO: spawn bounce object here later
    }

    // =========================
    // UTILITY
    // =========================

    void ClearLine()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
    } 
}