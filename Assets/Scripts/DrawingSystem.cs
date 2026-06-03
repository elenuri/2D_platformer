using System.Collections.Generic;
using UnityEngine;

public class DrawingSystem : MonoBehaviour
{
    [Header("Line Settings")]
    public LineRenderer lineRenderer;
    public float minPointDistance = 0.1f;

    [Header("Platform Settings")]
    public GameObject platformPrefab;
    public float maxPlatformLength = 4f;

    [Header("Shape Detection")]
    public float lineTolerance = 0.2f;

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
        Vector2 mousePos =
            cam.ScreenToWorldPoint(Input.mousePosition);

        if (points.Count > 0)
        {
            if (GetCurrentLineLength() >= maxPlatformLength)
            {
                return;
            }
        }

        if (
            points.Count == 0 ||
            Vector2.Distance(
                points[points.Count - 1],
                mousePos
            ) > minPointDistance
        )
        {
            points.Add(mousePos);

            lineRenderer.positionCount = points.Count;

            lineRenderer.SetPosition(
                points.Count - 1,
                mousePos
            );
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

        if (IsMostlyStraightLine(points))
        {
            HandleLine();
        }
        else
        {
            Debug.Log("Unrecognized shape");
        }

        Invoke(nameof(ClearLine), 0.1f);
    }

    float GetCurrentLineLength()
    {
        float length = 0f;

        for (int i = 1; i < points.Count; i++)
        {
            length += Vector2.Distance(
                points[i - 1],
                points[i]
            );
        }

        return length;
    }

    bool IsMostlyStraightLine(List<Vector2> pts)
    {
        if (pts.Count < 2)
            return false;

        Vector2 start = pts[0];
        Vector2 end = pts[pts.Count - 1];

        Vector2 direction =
            (end - start).normalized;

        for (int i = 1; i < pts.Count - 1; i++)
        {
            Vector2 pointDir =
                (pts[i] - start).normalized;

            float dot =
                Vector2.Dot(
                    direction,
                    pointDir
                );

            if (dot < 1 - lineTolerance)
            {
                return false;
            }
        }

        return true;
    }

    void HandleLine()
    {
        Debug.Log("Line detected");

        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
        }

        currentPlatform = Instantiate(platformPrefab);

        currentPlatform.transform.position =
            Vector3.zero;

        DrawnPlatform platform =
            currentPlatform.GetComponent<DrawnPlatform>();

        platform.Initialize(points);
    }

    void ClearLine()
    {
        points.Clear();

        lineRenderer.positionCount = 0;
    }
}