using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_MedalView : MonoBehaviour
{
    public Medal m_Medal;

    public void Set(int Id)
    {
        m_Medal.Clear();
        m_Medal.Set(Id);
    }

    public void OnClose()
    {
        
        gameObject.SetActive(false);
    }
}
