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
        m_Label_Stat_My[0].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Attack.ToString();
        m_Label_Stat_My[1].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Defence.ToString();
        m_Label_Stat_My[2].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Counter.ToString();
        m_Label_Stat_My[3].text = CM_Singleton<GameData>.instance.m_MyInfo.m_Special.ToString();

        m_Label_Stat_Other[0].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Attack.ToString();
        m_Label_Stat_Other[1].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Defence.ToString();
        m_Label_Stat_Other[2].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Counter.ToString();
        m_Label_Stat_Other[3].text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Special.ToString();
    }
}
