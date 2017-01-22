using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class DynamicTextGroup : MonoBehaviour
{
    [SerializeField] private List<DynamicText> _dynamicTexts;
    [SerializeField] private Vector2 _size = new Vector2(80, 100);
    [SerializeField] private float _duration = 0.5f;

    private RectTransform _rectTransform;

    public string test;
    private float _timer;
    public float time;
    public int count;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        SetText(test);
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= time)
        {
            _timer = 0;
            count++;
            SetText(count.ToString());
        }
    }

    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        _rectTransform.sizeDelta = new Vector2(text.Length * _size.x, _size.y);
        UpdateDynamicText(text);
    }

    private void UpdateDynamicText(string text)
    {
        if (_dynamicTexts.Count < text.Length)
        {
            _dynamicTexts.Add(Instantiate<DynamicText>(_dynamicTexts[0]));
            _dynamicTexts[_dynamicTexts.Count - 1].transform.SetParent(_rectTransform);
            _dynamicTexts[_dynamicTexts.Count - 1].transform.localScale = Vector3.one;
        }

        for (int cnt = 0; cnt < _dynamicTexts.Count; cnt++)
        {
            _dynamicTexts[cnt].GetComponent<RectTransform>().sizeDelta = _size;
            _dynamicTexts[cnt].Distance = _size.y * 1.5f;
            _dynamicTexts[cnt].Duration = _duration;
            _dynamicTexts[cnt].gameObject.SetActive(cnt < text.Length);
            if (cnt < text.Length)
            {
                _dynamicTexts[cnt].SetText(text[cnt].ToString());
            }
        }
    }
}
