using System.Collections.Generic;
using UnityEngine;

public class DrawingSystem : MonoBehaviour
{
    [Header("Line Settings")]
    public LineRenderer lineRenderer;
    public float minPointDistance = 0.1f;

    [Header("Shape Detection")]
    public float circleThreshold = 0.5f;
    public float lineTolerance = 0.2f;
    public float circleTolerance = 0.3f;

    [Header("Prefabs")]
    public GameObject platformPrefab;

    private List<Vector2> points = new List<Vector2>();
    private Camera cam;
    private bool isDrawing = false;

    private GameObject currentPlatform;

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
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            UpdateDrawing();
        }

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

        if (points.Count < 2)
        {
            ClearLine();
            return;
        }

        int closeIndex = -1;

        for (int i = 5; i < points.Count; i++)
        {
            float dist = Vector2.Distance(points[0], points[i]);

            if (dist < circleThreshold)
            {
                closeIndex = i;
                break;
            }
        }

        bool isClosed = closeIndex != -1;

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

        Invoke(nameof(ClearLine), 0.1f);
    }

    // =========================
    // SHAPE DETECTION
    // =========================

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

            if (dot < 1 - lineTolerance)
            {
                return false;
            }
        }

        return true;
    }

    bool IsRoughCircle(List<Vector2> pts)
    {
        if (pts.Count < 8) return false;

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

        if (ratio < 0.3f || ratio > 3f)
        {
            return false;
        }

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

        // Destroy old platform
        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
        }

        // Spawn new platform
        currentPlatform = Instantiate(platformPrefab);
        currentPlatform.transform.position = Vector3.zero;

        // Initialize platform
        DrawnPlatform platform = currentPlatform.GetComponent<DrawnPlatform>();
        platform.Initialize(points);
    }

    void HandleCircle()
    {
        Debug.Log("Circle detected");

        // Placeholder for later
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