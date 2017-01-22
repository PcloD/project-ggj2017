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
    private Vector3 _currentRadius;

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
        UpdateRadius();

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
        _transform.localScale = (_curPattern.Evaluate(_timer) * _currentRadius.x + _minCheckingCircle.localScale.x) * Vector3.one;

        if (_timer >= 2f)
        {
            OnLossGame();
        }
    }

    private void OnCheckCircle()
    {
        if (Input.GetKeyDown(GameManager.Instance.TriggerKey))
        {
            if (_transform.localScale.x >= _minCheckingCircle.localScale.x -0.1f && _transform.localScale.x <= _maxCheckingCircle.localScale.x+0.1f)
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
        AudioManager.Instance.OnCheckSuccess();
        GameManager.Instance.AddScore();
        _curPattern = GameManager.Instance.GetCurrentPattern();

        CreateClickEffect(true);
        UpdateRadius();
    }

    private void OnLossGame()
    {
        AudioManager.Instance.OnCheckFailure();
        _start = false;
        UIManager.Instance.OnLossGame();

		AchieveManager.Instance.SetHightestScore (GameManager.Instance.Score);
		HttpRequestManager.Instance.Upload(AchieveManager.Instance.GetNickName(), GameManager.Instance.Score.ToString());

        CreateClickEffect(false);
    }

    public void OnStartGame()
    {
        Reset();
        _start = true;
    }

    private void CreateClickEffect(bool success)
    {
        GameObject clickEffect = Instantiate(_clickEffect);
        clickEffect.transform.SetParent(_transform);
        clickEffect.transform.localPosition = Vector3.zero;
        clickEffect.GetComponent<ClickEffect>().Initialize(success);
    }

    private void UpdateRadius()
    {
        _currentRadius = RadiusDataManager.Instance.GetScales();
        _maxCheckingCircle.transform.localScale = Vector3.one * _currentRadius.y;
        _minCheckingCircle.transform.localScale = Vector3.one * _currentRadius.z;
    }
}
