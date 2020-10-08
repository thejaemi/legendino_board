using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int m_Id;
    public string m_Name;
    public int m_Attribute;
    public int m_Rank;
    public int m_Attack;
    public int m_Defence;
    public int m_Counter;
    public int m_Special;
    public int m_Hp;

    public Card(string[] val)
    {
        m_Id = int.Parse(val[0]);
        m_Name = val[1];
        m_Attribute = int.Parse(val[2]);
        m_Rank = int.Parse(val[3]);
        m_Attack = int.Parse(val[4]);
        m_Defence = int.Parse(val[5]);
        m_Counter = int.Parse(val[6]);
        m_Special = int.Parse(val[7]);
        m_Hp = int.Parse(val[8]);
    }
}
