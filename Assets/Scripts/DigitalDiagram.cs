using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DigitalDiagram : MonoBehaviour
{
    [SerializeField] private Transform _anchorLeft;
    [SerializeField] private Transform _anchorRight;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Transform _basicPattern;
    [SerializeField] private Transform _minCheckingCircle;

    [SerializeField] private int _sliceNumber = 400;
    [SerializeField] private int _amplitudeMultiplier = 10;

    private float _distance = 100;

    private LineRenderer _lineRenderer;
    private Vector3[] _vertices;
    private float _inverse = 1;

    private void Awake()
    {
        _distance = Vector3.Distance(_anchorLeft.position, _anchorRight.position);
        _distance /= 2;

        _lineRenderer = GetComponent<LineRenderer>();
        _vertices = new Vector3[_sliceNumber];

        for (int cnt = 0; cnt < _sliceNumber; cnt++)
        {
            _vertices[cnt] = new Vector3(-_distance + cnt * _distance * 2 / _sliceNumber, 0, _canvas.position.z + 1);
        }

        _lineRenderer.numPositions = _sliceNumber;
        _lineRenderer.SetPositions(_vertices);
    }

    private void Update()
    {
        _inverse = GameManager.Instance.Score % 2 == 0 ? 1 : -1;
        UpdateAmplitude(_inverse * (_basicPattern.localScale.x - _minCheckingCircle.localScale.x));
    }

    private void UpdateAmplitude(float latestAmplitude)
    {
        for (int cnt = 0; cnt < _sliceNumber - 1; cnt++)
        {
            _vertices[cnt].y = _vertices[cnt + 1].y;
        }

        _vertices[_sliceNumber - 1].y = latestAmplitude * _amplitudeMultiplier;
        _lineRenderer.SetPositions(_vertices);
    }
}
