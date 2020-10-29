using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Enable_Run : MonoBehaviour
{
    public Event_Base[] m_List;

    private void OnEnable()
    {
        for (int i = 0; i < m_List.Length; i++)
            m_List[i].Run();
    }
}
