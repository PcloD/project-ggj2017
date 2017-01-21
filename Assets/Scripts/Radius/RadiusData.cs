using System;

[Serializable]
public class RadiusData
{
    public float m_redMinScale;
    public float m_redMaxScale;
    public float m_greenMinScale;
    public float m_greenMaxScale;
    public float m_lessGreenScale;

    public RadiusData()
    {
        m_redMinScale = 2.0f;
        m_redMaxScale = 2.0f;
        m_greenMinScale = 1.0f;
        m_greenMaxScale = 1.0f;

        m_lessGreenScale = 0.0f;
    }
}
