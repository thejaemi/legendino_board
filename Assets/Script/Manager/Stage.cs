using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public int m_Id;
    public int m_StageId;
    public string m_StageName;
    public int m_Dino1;
    public int m_Dino2;
    public int m_Dino3;
    public int m_Card1;
    public int m_Card2;
    public int m_Card3;
    public int m_Card4;
    public int m_Card5;
    public string m_StageImage;

    public Stage(string[] val)
    {
        m_Id = int.Parse(val[0]);
        m_StageId = int.Parse(val[1]);
        m_StageName = val[2];
        m_Dino1 = int.Parse(val[3]);
        m_Dino2 = int.Parse(val[4]);
        m_Dino3 = int.Parse(val[5]);
        m_Card1 = int.Parse(val[6]);
        m_Card2 = int.Parse(val[7]);
        m_Card3 = int.Parse(val[8]);
        m_Card4 = int.Parse(val[9]);
        m_Card5 = int.Parse(val[10]);
        m_StageImage = val[11];
    }
}
