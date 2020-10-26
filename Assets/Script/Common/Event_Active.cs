using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Active : MonoBehaviour
{
    public GameObject m_Target;
    public bool m_Active = true;

    public void Run()
    {
        if (m_Target)
            m_Target.SetActive(m_Active);
    }
}
