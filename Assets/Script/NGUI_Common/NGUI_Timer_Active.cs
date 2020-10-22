using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUI_Timer_Active : MonoBehaviour
{
    public bool m_bActive = false;
    public float m_fSec = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_fSec != 0.0f)
        {
            m_fSec -= Time.deltaTime;
            if (m_fSec <= 0.0f)
                gameObject.SetActive(m_bActive);
        }
    }
}
