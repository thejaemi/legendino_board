using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI_Gauge : MonoBehaviour
{
    public Image m_Image_Gauge;
    public Image m_Image_PreGauge;
    float m_Ratio = 1.0f;
    public bool m_UsePreGauge = true;
    public float m_RunTime = 1.0f;
    public bool m_Debug = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDefault()
    {
        m_Ratio = 1.0f;
        m_Image_Gauge.fillAmount = m_Ratio;
        m_Image_PreGauge.fillAmount = m_Ratio;
    }

    public void Set(float Ratio)
    {
        if(m_UsePreGauge)
        {
            if (m_Ratio > Ratio)
            {
                m_Ratio = Ratio;
                StartCoroutine(Down(m_RunTime));
            }
            else
            {
                m_Ratio = Ratio;
                StartCoroutine(Up(m_RunTime));
            }
        }
        else
        {
            m_Ratio = Ratio;
            m_Image_Gauge.fillAmount = m_Ratio;
            m_Image_PreGauge.fillAmount = m_Ratio;
        }
    }

    IEnumerator Down(float RunTime)
    {
        float StartTime = Time.time;
        float Start = m_Image_PreGauge.fillAmount;

        m_Image_Gauge.fillAmount = m_Ratio;

        while (true)
        {
            if (gameObject == null)
                break;

            m_Image_PreGauge.fillAmount = Mathf.Lerp(Start, m_Ratio, (Time.time - StartTime) / RunTime);

            if (Mathf.Approximately(m_Image_PreGauge.fillAmount, m_Ratio))
                break;

            yield return null;
        }

        yield break;
    }

    IEnumerator Up(float RunTime)
    {
        float StartTime = Time.time;
        float Start = m_Image_Gauge.fillAmount;

        m_Image_PreGauge.fillAmount = m_Ratio;

        while (true)
        {
            if (gameObject == null)
                break;

            m_Image_Gauge.fillAmount = Mathf.Lerp(Start, m_Ratio, (Time.time - StartTime) / RunTime);

            if (Mathf.Approximately(m_Image_Gauge.fillAmount, m_Ratio))
                break;

            yield return null;
        }

        yield break;
    }

    private void OnGUI()
    {
        if (!m_Debug)
            return;

        if (GUI.Button(new Rect(50, 50, 100, 100), "Down"))
            Set(0.5f);

        if (GUI.Button(new Rect(50, 150, 100, 100), "Up"))
            Set(1.0f);
    }
}
