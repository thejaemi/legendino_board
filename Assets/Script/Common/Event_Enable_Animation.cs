using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Enable_Animation : Event_Base
{
    public Animation m_Anim;
    public string m_AniName;

    private void OnEnable()
    {
        if (m_AniName == "")
            m_Anim.Play();
        else
            m_Anim.Play(m_AniName);
    }
}
