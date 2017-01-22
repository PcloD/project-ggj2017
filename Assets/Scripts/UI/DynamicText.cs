using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DynamicText : MonoBehaviour
{
    public float Distance { set { _distance = value; } }
    public float Duration { set { _duration = value; } }

    [SerializeField] private Text _displayText;
    [SerializeField] private Text _prepareText;

    private float _distance = 300;
    private float _duration = 0.5f;
    private int _count;
    private Tweener _displayTweener;
    private Tweener _prepareTweener;

    public void SetText(string text)
    {
        if (_prepareText.text == text)
        {
            return;
        }

        _displayTweener.Kill();
        _prepareTweener.Kill();
        StopCoroutine(ResetText());

        ResetPosition();

        _prepareText.text = text;
        _displayTweener = _displayText.transform.DOLocalMoveY(-_distance, _duration);
        _prepareTweener = _prepareText.transform.DOLocalMoveY(0, _duration);

        StartCoroutine(ResetText());
    }

    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(_duration);

        _displayText.text = _prepareText.text;
    }

    private void ResetPosition()
    {
        _displayText.transform.localPosition = Vector3.zero;
        _prepareText.transform.localPosition = Vector3.up * _distance;
    }
}
