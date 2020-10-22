using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NGUI_Text_Alpha : MonoBehaviour {

    Text m_Text;
    Color m_Color;

    public float m_fAlphaStart = 0.0f;
    public float m_fAlphaEnd = 1.0f;
    [ReadOnly]
    public float m_fAlpha;
    public float m_fSec = 1.5f;
    float m_fMoveSec = 0.0f;

    void Awake()
    {
        m_Text = GetComponent<Text>();
        m_Color = m_Text.color;
    }

    void Start()
    {
        m_fAlpha = m_fAlphaStart;
        m_Color = new Color(m_Color.r, m_Color.g, m_Color.b, m_fAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fAlphaStart < m_fAlphaEnd)
        {
            if (m_fAlpha < m_fAlphaEnd)
            {
                m_fMoveSec += m_fSec * Time.deltaTime;
                m_fAlpha = Mathf.Lerp(m_fAlphaStart, m_fAlphaEnd, m_fMoveSec);
                m_Text.color = new Color(m_Color.r, m_Color.g, m_Color.b, m_fAlpha);
            }
        }
        else
        {
            if (m_fAlpha > m_fAlphaEnd)
            {
                m_fMoveSec -= m_fSec * Time.deltaTime;
                m_fAlpha = Mathf.Lerp(m_fAlphaEnd, m_fAlphaStart, m_fMoveSec);
                m_Text.color = new Color(m_Color.r, m_Color.g, m_Color.b, m_fAlpha);
            }
        }
    }

    public void Reset()
    {
        if (m_fAlphaStart < m_fAlphaEnd)
            m_fMoveSec = 0.0f;
        else
            m_fMoveSec = 1.0f;
        m_fAlpha = m_fAlphaStart;
    }

    void OnEnable()
    {
        Reset();
    }
}
