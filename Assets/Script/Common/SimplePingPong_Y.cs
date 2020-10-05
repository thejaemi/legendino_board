using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePingPong_Y : MonoBehaviour
{
    public bool m_bPause = false;
    public float m_Ratio = 0.02f;
    Vector3 m_Origin;

    private void Awake()
    {
        m_Origin = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bPause)
        {
            gameObject.transform.localScale = m_Origin;
            return;
        }

        gameObject.transform.position = m_Origin + Vector3.up * Mathf.PingPong(Time.time, 1f) * m_Ratio;
    }
}
