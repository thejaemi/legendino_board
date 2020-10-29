using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Enable_Active : Event_Base
{
    public GameObject[] m_Target;
    public bool m_Active = false;
    public float m_Delay = 0.0f;

    private void OnEnable()
    {
        Invoke("Run", m_Delay);
    }

    public override void Run()
    {
        for(int i=0; i<m_Target.Length; i++)
            m_Target[i].SetActive(m_Active);
    }
}
