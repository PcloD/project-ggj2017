using UnityEngine;

public class BasicPattern : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationPattern;
    [SerializeField] private float _maxScale = 2f;
    [SerializeField] private Transform _minCheckingCircle;
    [SerializeField] private Transform _maxCheckingCircle;
    [SerializeField] private float _speed = 1.0f;

    private Transform _transform;
    private bool _start = false;
    private float _timer = 0;

    private void Awake()
    {
        _transform = transform;
        Reset();
    }

    private void Reset()
    {
        _timer = 0;
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

        _timer += Time.deltaTime * _speed;
        _transform.localScale = (_animationPattern.Evaluate(_timer) + _minCheckingCircle.localScale.x) * Vector3.one;

        if (_timer >= 2f)
        {
            OnLossGame();
        }
    }

    private void OnCheckCircle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_transform.localScale.x >= _minCheckingCircle.localScale.x && _transform.localScale.x <= _maxCheckingCircle.localScale.x)
            {
                _timer = 2 - _timer;
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
