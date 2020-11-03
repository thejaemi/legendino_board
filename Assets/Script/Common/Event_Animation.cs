using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Animation : Event_Base
{
    public Animation m_Anim;
    public string m_AniName;
    public float m_Delay = 0.0f;

    override public void Run()
    {
        if (m_Delay > 0.0f)
            Invoke("Run_", m_Delay);
        else
            Run_();
    }

    public void Run(float Delay)
    {
        if (m_Delay > 0.0f)
            Invoke("Run_", Delay);
        else
            Run_();
    }

    void Run_()
    {
        if (m_AniName == "")
            m_Anim.Play();
        else
            m_Anim.Play(m_AniName);
    }
}
