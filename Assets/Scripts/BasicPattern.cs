using UnityEngine;

public class BasicPattern : MonoBehaviour
{
    [SerializeField] private float _maxScale = 2f;
    [SerializeField] private Transform _minCheckingCircle;
    [SerializeField] private Transform _maxCheckingCircle;
    [SerializeField] private float _scaleSpeed = 0.25f;

    private Transform _transform;
    private bool _scaleUp = true;
    private bool _start = false;

    private void Awake()
    {
        _transform = transform;
        Reset();
    }

    private void Reset()
    {
        _scaleUp = true;
        _start = false;
        _transform.localScale = Vector3.one * _minCheckingCircle.localScale.x;
    }

    private void Update()
    {
        if (!_start)
        {
            return;
        }

        OnCheckCircle();

        if (_scaleUp)
        {
            _transform.localScale += Vector3.one * _scaleSpeed * Time.deltaTime;

            if (_transform.localScale.x >= _maxScale)
            {
                _scaleUp = false;
            }
        }
        else
        {
            _transform.localScale -= Vector3.one * _scaleSpeed * Time.deltaTime;

            if (_transform.localScale.x < _minCheckingCircle.localScale.x)
            {
                OnLossGame();
            }
        }
    }

    private void OnCheckCircle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_transform.localScale.x >= _minCheckingCircle.localScale.x && _transform.localScale.x <= _maxCheckingCircle.localScale.x)
            {
                _scaleUp = true;
                OnGetScore();
            }
            else
            {
                OnLossGame();
            }
        }
    }

    private void OnGetScore()
    {
        UIManager.Instance.OnGetScore();
    }

    private void OnLossGame()
    {
        _start = false;
        UIManager.Instance.OnLossGame();
    }

    public void OnStartGame()
    {
        Reset();
        _start = true;
    }
}
