using UnityEngine;

public class BasicPattern : MonoBehaviour
{
    [SerializeField] private Transform _minCheckingCircle;
    [SerializeField] private Transform _maxCheckingCircle;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private GameObject _clickEffect;

    private Transform _transform;
    private bool _start = false;
    private float _timer = 0;
    private AnimationCurve _curPattern;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        _timer = 0;
        _start = false;
        _transform.localScale = Vector3.one * _minCheckingCircle.localScale.x;
        _curPattern = GameManager.Instance.GetCurrentPattern();
    }

    private void Update()
    {
        if (!_start)
        {
            return;
        }

        OnCheckCircle();

        _timer += Time.deltaTime * _speed;
        _transform.localScale = (_curPattern.Evaluate(_timer) + _minCheckingCircle.localScale.x) * Vector3.one;

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
        GameManager.Instance.AddScore();
        _curPattern = GameManager.Instance.GetCurrentPattern();

        CreateClickEffect(Color.white);
    }

    private void OnLossGame()
    {
        _start = false;
        UIManager.Instance.OnLossGame();

		AchieveManager.Instance.SetHightestScore (GameManager.Instance.Score);


        CreateClickEffect(Color.red);
    }

    public void OnStartGame()
    {
        Reset();
        _start = true;
    }

    private void CreateClickEffect(Color color)
    {
        GameObject clickEffect = Instantiate(_clickEffect);
        clickEffect.transform.SetParent(_transform);
        clickEffect.transform.localPosition = Vector3.zero;
        clickEffect.GetComponent<ClickEffect>().Initialize(color);
    }
}
