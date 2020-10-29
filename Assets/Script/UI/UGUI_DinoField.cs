using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUI_DinoField : MonoBehaviour
{
    public UGUI_Medal m_Medal;
    public GameObject m_SpinBase;
    public GameObject m_BackLight;
    public GameObject m_Arrow;
    private Dinosaur m_dinosaur;
    public bool m_ShowTool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        m_SpinBase.transform.rotation = m_Medal.transform.rotation;
    }

    public void SetDinoMedal(Dinosaur _dinosaur)
    {
        m_dinosaur = _dinosaur;
        m_Medal.Set(m_dinosaur.index);
    }

    private void ActionAttack()
    {

    }

    public void OnClickDinoField()
    {

    }

    public void StartSpin()
    {
        OnArrow();
        m_Medal.OnSpin();
    }

    public void StopSpin()
    {
        Invoke("OffArrow", 1.5f);
        m_Medal.OffSpin();
    }

    public void OnBackLight()
    {
        m_BackLight.SetActive(true);
    }

    void OnArrow()
    {
        m_Arrow.SetActive(true);
    }

    void OffArrow()
    {
        m_Arrow.SetActive(false);
    }

    public Dino GetDino()
    {
        return m_Medal.m_Dino;
    }

    public int GetCommand()
    {
        return m_Medal.Get();
    }

    private void OnGUI()
    {
        if (m_ShowTool == false)
            return;

        if (GUI.Button(new Rect(400, 100, 100, 30), "OnSpin"))
            StartSpin();

        if (GUI.Button(new Rect(400, 140, 100, 30), "OffSpin"))
            StopSpin();
    }
}
