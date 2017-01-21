using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformScaleShowHide : AbsRectTransformShowHideAction
{
    [SerializeField]
    private float m_ScaleTime = 0.3f;
    private Vector3 m_OriginalInitialScale;
    // Use this for initialization
    private float m_startTime;
    private Vector3 m_Position;
    private Rect m_Rect;
    private RectTransform m_RectTransform;
    private Vector2 m_OffsetMin;
    private Vector2 m_OffsetMax;
    private Vector2 m_OriginalAncheredPos;
    private Vector2 m_TargetAncheredPos;
    private Vector2 m_AncheredPosScaleRef;
    void Start()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        m_Position = transform.position;
        m_OriginalInitialScale = transform.localScale;
        m_Rect = m_RectTransform.rect;
        m_OffsetMin = m_RectTransform.offsetMin;
        m_OffsetMax = m_RectTransform.offsetMax;
        m_OriginalAncheredPos = m_RectTransform.anchoredPosition;
        m_TargetAncheredPos = (m_OffsetMax + m_OffsetMin) / 2.0f;
        m_AncheredPosScaleRef = m_TargetAncheredPos - m_OriginalAncheredPos;
    }

    public float scaleTime
    {
        set
        {
            m_ScaleTime = value;
        }
    }

    public override void Show(params object[] options)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        m_startTime = Time.time;
        StartCoroutine(_show());
    }

    public override void Hide(params object[] options)
    {
        StopAllCoroutines();
        m_startTime = Time.time;
        StartCoroutine(_hide());
    }

    private IEnumerator _hide()
    {
        float startTime = Time.time;
        yield return null;
        while (Time.time - startTime < m_ScaleTime)
        {
            float scale = 1.0f - (Time.time - startTime) / m_ScaleTime;
            if (scale < 0.0)
            {
                break;
            }
            transform.localScale = m_OriginalInitialScale * scale;
            transform.GetComponent<RectTransform>().anchoredPosition = m_OriginalAncheredPos + m_AncheredPosScaleRef * (1 - scale);
            yield return null;
        }
        transform.localScale = Vector3.zero;
        transform.GetComponent<RectTransform>().anchoredPosition = m_TargetAncheredPos;
        if (hideCompleteCallback != null)
        {
            hideCompleteCallback();
        }
        gameObject.SetActive(false);
    }

    private IEnumerator _show()
    {
        float startTime = Time.time;
        yield return null;
        while (Time.time - startTime < m_ScaleTime)
        {
            float scale = (Time.time - startTime) / m_ScaleTime;
            if (scale > 1.0)
            {
                break;
            }
            transform.GetComponent<RectTransform>().localScale = m_OriginalInitialScale * scale;
            transform.GetComponent<RectTransform>().anchoredPosition = m_OriginalAncheredPos + m_AncheredPosScaleRef * (1 - scale);
            yield return null;
        }

        transform.GetComponent<RectTransform>().localScale = m_OriginalInitialScale;
        transform.GetComponent<RectTransform>().anchoredPosition = m_OriginalAncheredPos;
        
        if(showCompleteCallback != null)
        {
            showCompleteCallback();
        }
    }
}
