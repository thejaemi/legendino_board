using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public List<int> m_Dino = new List<int>();

    public int m_Hp = 0;
    public int m_Attack = 0;
    public int m_Defence = 0;
    public int m_Counter = 0;
    public int m_Special = 0;
    public void Reset_Stat() { m_Hp = 0; m_Attack = 0; m_Defence = 0; m_Counter = 0; m_Special = 0; }

    public int m_Equip_Amulet;
    public int m_Equip_Ring;
    public int m_Equip_Glove;
    public int m_Equip_Map;
    public int m_Equip_Belt;
    public void Reset_Equip() { m_Equip_Amulet = 0; m_Equip_Ring = 0; m_Equip_Glove = 0; m_Equip_Map = 0; m_Equip_Belt = 0; }


    public void Add_Dino(int Id)
    {
        m_Dino.Add(Id);

        CalcStat();
    }

    public void Clear_Dino()
    {
        m_Dino.Clear();

        CalcStat();
    }

    public void Set_Card(int Type, int Id)
    {

        switch (Type)
        {
            case 1:
                m_Equip_Amulet = Id;
                break;

            case 2:
                m_Equip_Ring = Id;
                break;

            case 3:
                m_Equip_Glove = Id;
                break;

            case 4:
                m_Equip_Map = Id;
                break;

            case 5:
                m_Equip_Belt = Id;
                break;
        }

        CalcStat();
    }

    /// <summary>
    /// 스텟 계산
    /// </summary>
    public void CalcStat()
    {
        Reset_Stat();

        for (int i = 0; i < m_Dino.Count; i++)
        {
            m_Hp += CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[m_Dino[i]].m_Hp;
            m_Attack += CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[m_Dino[i]].m_Attack;
            m_Defence += CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[m_Dino[i]].m_Defence;
            m_Special += CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[m_Dino[i]].m_Special;
        }

        CalcCardStat(m_Equip_Amulet);
        CalcCardStat(m_Equip_Belt);
        CalcCardStat(m_Equip_Glove);
        CalcCardStat(m_Equip_Map);
        CalcCardStat(m_Equip_Ring);
    }

    public void CalcCardStat(int Id)
    {
        if (Id == 0)
            return;

        m_Hp += CM_Singleton<GameData>.instance.m_Table_Card.m_Dic[Id].m_Hp;
        m_Attack += CM_Singleton<GameData>.instance.m_Table_Card.m_Dic[Id].m_Attack;
        m_Defence += CM_Singleton<GameData>.instance.m_Table_Card.m_Dic[Id].m_Defence;
        m_Special += CM_Singleton<GameData>.instance.m_Table_Card.m_Dic[Id].m_Special;
    }
}
