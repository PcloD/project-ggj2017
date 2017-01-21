using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DaynamicText : MonoBehaviour
{
    [SerializeField] private Text _displayText;
    [SerializeField] private Text _prepareText;
    [SerializeField] private float _distance = 300;
    [SerializeField] private float _duration = 0.5f;

    private int _count;
    private Tweener _displayTweener;
    private Tweener _prepareTweener;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _count++;
            SetText(_count.ToString());
        }
    }

    public void SetText(string text)
    {
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
