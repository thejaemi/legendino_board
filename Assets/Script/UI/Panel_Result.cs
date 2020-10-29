using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Panel_Result : MonoBehaviour
{
    public SkeletonGraphic m_Spine_Start;
    public SkeletonGraphic m_Spine_Fin;
    public SkeletonGraphic m_Spine_Win;
    public SkeletonGraphic m_Spine_Draw;
    public SkeletonGraphic m_Spine_Lose;
    public GameObject m_Button;

    public void Reset()
    {
        m_Spine_Start.gameObject.SetActive(false);
        m_Spine_Fin.gameObject.SetActive(false);
        m_Spine_Win.gameObject.SetActive(false);
        m_Spine_Draw.gameObject.SetActive(false);
        m_Spine_Lose.gameObject.SetActive(false);
        m_Button.SetActive(false);
    }

    public void OnStart()
    {
        Reset();
        m_Spine_Start.gameObject.SetActive(true);
        Invoke("ActiveButton", 2.0f);
    }

    public void OnFin()
    {
        Reset();
        m_Spine_Fin.gameObject.SetActive(true);
        Invoke("ActiveButton", 2.0f);
    }

    public void OnWin()
    {
        Reset();
        m_Spine_Win.gameObject.SetActive(true);
        Invoke("ActiveButton", 2.0f);
    }

    public void OnDraw()
    {
        Reset();
        m_Spine_Draw.gameObject.SetActive(true);
        Invoke("ActiveButton", 2.0f);
    }

    public void OnLose()
    {
        Reset();
        m_Spine_Lose.gameObject.SetActive(true);
        Invoke("ActiveButton", 2.0f);
    }

    public void ActiveButton()
    {
        m_Button.SetActive(true);
    }
}
