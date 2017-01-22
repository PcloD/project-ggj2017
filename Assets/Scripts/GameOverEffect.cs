using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameOverEffect : MonoBehaviour {
    public Action onEffectCompleteCallback; 
    [SerializeField]
    private CameraLerpGrayscaleRatio m_CameraLerpGrayscaleRatio;

    [SerializeField]
    private float m_EffectTime;

    public void StartGameOverEffect()
    {
        StopAllCoroutines();
        StartCoroutine(gameOverEffect());
    }

    private IEnumerator gameOverEffect()
    {
        float startTime = Time.time;
        yield return null;
        while(Time.time - startTime < m_EffectTime)
        {
            float ratio = (Time.time - startTime) / m_EffectTime;
            if(ratio > 0.0f)
            {
                m_CameraLerpGrayscaleRatio.grayscaleRatio = ratio;
            }
            yield return null;
        }
        m_CameraLerpGrayscaleRatio.grayscaleRatio = 1.0f;
        if(onEffectCompleteCallback != null)
        {
            onEffectCompleteCallback();
        }
    }

    public void ResetGameOverEffect()
    {
        m_CameraLerpGrayscaleRatio.grayscaleRatio = 0.0f;
    }
}
