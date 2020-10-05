using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePingPong_Scale : MonoBehaviour
{
    public bool m_bPause = false;

    Vector3 m_LocalScale;

    private void Awake()
    {
        m_LocalScale = transform.localScale;
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
            gameObject.transform.localScale = m_LocalScale;
            return;
        }

        gameObject.transform.localScale = m_LocalScale * (0.9f + (Mathf.PingPong(Time.time, 1f) * 0.1f));

        //gameObject.transform.localRotation = Quaternion.Euler(m_LocalScale * f);
    }
}
