using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino
{
    public int m_Index;
    public string m_Name;
    public int m_Attack;
    public int m_Defence;
    public int m_Counter;
    public int m_Special;
    public int m_Hp;
    public float m_Ratio_Attack;
    public float m_Ratio_Defence;
    public float m_Ratio_Counter;
    public float m_Ratio_Evade;
    public float m_Ratio_Special;
    public string m_Ani_Attack;
    public string m_Ani_Special;

    public Dino(string[] val)
    {
        m_Index = int.Parse(val[1]);
        m_Name = val[2];
        m_Attack = int.Parse(val[3]);
        m_Defence = int.Parse(val[4]);
        m_Counter = int.Parse(val[5]);
        m_Special = int.Parse(val[6]);
        m_Hp = int.Parse(val[7]);
        m_Ratio_Attack = float.Parse(val[8]);
        m_Ratio_Defence = float.Parse(val[9]);
        m_Ratio_Counter = float.Parse(val[10]);
        m_Ratio_Evade = float.Parse(val[11]);
        m_Ratio_Special = float.Parse(val[12]);
        if (Mathf.Approximately(1.0f, m_Ratio_Attack + m_Ratio_Defence + m_Ratio_Counter + m_Ratio_Evade + m_Ratio_Special) == false)
            Debug.LogErrorFormat("Dino {0} Ratio's Total Not 1  ({1})", m_Index, m_Ratio_Attack + m_Ratio_Defence + m_Ratio_Counter + m_Ratio_Evade + m_Ratio_Special);
        m_Ani_Attack = val[13];
        m_Ani_Special = val[14];
    }
}
