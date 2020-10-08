using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoField : MonoBehaviour
{
    public Medal m_Medal;
    private Dinosaur m_dinosaur;
    public bool m_ShowTool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

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
        m_Medal.OnSpin();
    }

    public void StopSpin()
    {
        m_Medal.OffSpin();
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
