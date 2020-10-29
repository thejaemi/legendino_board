using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Active : Event_Base
{
    public GameObject m_Target;
    /// <summary>
    /// true = 활성 false = 비활성
    /// </summary>
    public bool m_Active = true;
    public float m_Delay = 0.0f;

    override public void Run()
    {
        if (m_Delay > 0.0f)
            Invoke("Run_", m_Delay);
        else
            Run_();
    }

    void Run_()
    {
        if (m_Target)
            m_Target.SetActive(m_Active);
    }
}
