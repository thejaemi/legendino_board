using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Blind : MonoBehaviour
{
    public Text[] m_Label_Stat_My;
    public Text[] m_Label_Stat_Other;

    public void Reflash()
    {
        m_Label_Stat_My[0].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Stat.m_Attack.ToString();
        m_Label_Stat_My[1].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Stat.m_Defence.ToString();
        m_Label_Stat_My[2].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Stat.m_Counter.ToString();
        m_Label_Stat_My[3].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Stat.m_Special.ToString();

        m_Label_Stat_Other[0].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Stat.m_Attack.ToString();
        m_Label_Stat_Other[1].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Stat.m_Defence.ToString();
        m_Label_Stat_Other[2].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Stat.m_Counter.ToString();
        m_Label_Stat_Other[3].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Stat.m_Special.ToString();
    }
}
