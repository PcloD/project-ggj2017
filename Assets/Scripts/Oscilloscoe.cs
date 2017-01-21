using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilloscoe : MonoBehaviour {
    public static Oscilloscoe Instance;

    public LineRenderer lineRenderer
    {
        get
        {
            if (!_lineRenderer)
            {
                _lineRenderer = GetComponent<LineRenderer>();
            }
            return _lineRenderer;
        }
    }
    public float intervalTime;
    public bool updateLine = false;
    public float pointMoveSpeed;
    public float scale = 2;
    public float StartWidth;
    public float EndWidth;
    public Color LineColor;
    public Vector2 StartOnViewportPos;

    private float lastTime;
    private List<Vector3> points = new List<Vector3>();
    private LineRenderer _lineRenderer;
    private Camera _mainCamera;
    public Camera mainCamera
    {
        get {
            if (_mainCamera == null)
                _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            return _mainCamera;
        }
    }


    void Awake()
    {
        Instance = this;
        if(lineRenderer.material == null)
        {
            lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
            lineRenderer.material.color = LineColor;
        }
        lineRenderer.startWidth = StartWidth;
        lineRenderer.endWidth = EndWidth;
        //Vector3 viewPos = new Vector3(StartOnViewportPos.x * Screen.width, StartOnViewportPos.y * Screen.height, Camera.main.nearClipPlane);
        //Vector3 wpos = viewPos;
        //transform.position = wpos;
    }

    void Start()
    {
        GameStartStart();
    }

    public void GameStartStart()
    {
        points.Clear();
        updateLine = true;
    }

    void Update()
    {
        if(!updateLine)
            return;

        lastTime -= Time.deltaTime;
        if(lastTime <= 0)
        {
            lastTime = intervalTime;

            AddLinePoint();
        }
        UpdatPoints();
    }

    void AddLinePoint()
    {
        float value = GameManager.Instance.GetCurrentPattern().Evaluate(Time.time);
        Vector3 newPoint = new Vector3(transform.position.x, (value * scale) + transform.position.y , transform.position.z);
        points.Add(newPoint);
        if (points.Count > 70)
            points.RemoveAt(0);
    }

    void UpdatPoints() {
        //points = points.FindAll(e => /*mainCamera.WorldToViewportPoint(e).x > 0*/e.x <= -30 );
        lineRenderer.SetVertexCount(points.Count);
        for (int i = 0; i < points.Count; i++)
        {
            points[i] = new Vector3(points[i].x - (Time.deltaTime*pointMoveSpeed), points[i].y, points[i].z);
            lineRenderer.SetPosition(i,Camera.main.WorldToViewportPoint(points[i]));
        }
    }


}
