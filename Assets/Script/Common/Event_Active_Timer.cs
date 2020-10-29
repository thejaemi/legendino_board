using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Active_Timer : Event_Base
{
    public GameObject m_Target;
    public float m_Sec_Able = 0.0f;
    public float m_Sec_Disable = 0.0f;

    override public void Run()
    {
        Invoke("Event_Able", m_Sec_Able);
        if (m_Sec_Disable > 0.0f)
            Invoke("Event_Disable", m_Sec_Disable);
    }

    void Event_Able()
    {
        m_Target.SetActive(true);
    }

    void Event_Disable()
    {
        m_Target.SetActive(false);
    }
}
