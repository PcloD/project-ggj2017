using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilloscoe : MonoBehaviour {
    public static Oscilloscoe Instance;

    public Vector2 StartPoint;
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
    public float hight;
    public float minHeight;
    public float maxHeight;
    public float StartWidth;
    public float EndWidth;
    public Color LineColor;
    public Color LineStartColor;
    public Color LineEndColor;

    public int MaxNumber; 

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
    private Canvas _canvas;
    private Canvas getCanvas
    {
        get
        {
            if(_canvas == null)
                _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            return _canvas;
        }
    }


    void Awake()
    {
        Instance = this;
        lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
        lineRenderer.material.color = LineColor;
        lineRenderer.startColor = LineStartColor;
        lineRenderer.endColor = LineEndColor;
        lineRenderer.startWidth = StartWidth;
        lineRenderer.endWidth = EndWidth;
    }

    void Start()
    {
        GameStartStart();
    }

    public void GameStartStart()
    {
        points.Clear();
        updateLine = true;
        for(int i = 0; i< MaxNumber; i++)
        {
            AddLinePoint(i*intervalTime);
        }
    }

    void Update()
    {
        if(!updateLine)
            return;

        lastTime -= Time.deltaTime;
        if(lastTime <= 0)
        {
            lastTime = intervalTime;

            AddLinePoint(Time.time);
        }
        UpdatPoints();
    }

    void AddLinePoint(float time)
    {
        float value = GameManager.Instance.GetCurrentPattern().Evaluate(time);
        Vector3 newPoint = Vector3.up * value;
        points.Add(newPoint);
        if (points.Count > MaxNumber)
            points.RemoveAt(0);
    }

    void UpdatPoints() {
        //points = points.FindAll(e => /*mainCamera.WorldToViewportPoint(e).x > 0*/e.x <= -30 );
        lineRenderer.SetVertexCount(points.Count);
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 newPos = FixPos(i);
            lineRenderer.SetPosition(i, newPos);
        }
    }

    Vector3 FixPos(int index)
    {
        Vector3 newPos = new Vector3(((points.Count - index) * pointMoveSpeed) + (Time.deltaTime * pointMoveSpeed) + StartPoint.x, (points[index].y * Mathf.Clamp(hight * (points.Count - index), minHeight, maxHeight)) + Camera.main.transform.position.y + StartPoint.y, /*getCanvas.transform.position.z*/0);
        return newPos;
    }
}
