using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI_Alpha_PingPong : MonoBehaviour
{
    Image m_Image;
    Color m_Color;

    [ReadOnly]
    public float m_Alpha;

    void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Color = m_Image.color;
    }

    void Start()
    {
        m_Alpha = 0.0f;
        m_Color = new Color(m_Color.r, m_Color.g, m_Color.b, m_Alpha);
    }

    // Update is called once per frame
    void Update()
    {
        m_Alpha = Mathf.PingPong(Time.time, 1.0f);
        m_Image.color = new Color(m_Color.r, m_Color.g, m_Color.b, m_Alpha);
    }
}
