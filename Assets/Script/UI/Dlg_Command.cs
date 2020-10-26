using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dlg_Command : MonoBehaviour
{
    UGUI_Move m_Move;
    public Image m_Image_Mark;
    public Text m_Value;
    public Text m_ValueDesc;

    private void Awake()
    {
        m_Move = gameObject.GetComponent<UGUI_Move>();
    }

    public void Set(PlayerInfo MyInfo, int Command_My, PlayerInfo OtherInfo, int Command_Other)
    {
        if (Command_My == 0) // 공격
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_attack");
            m_Value.text = MyInfo.m_Stat.m_Attack.ToString();
            m_ValueDesc.text = string.Format("{0} + {1}", MyInfo.m_Stat_Card.m_Attack, MyInfo.m_Stat_Medal.m_Attack);

            /*
            if (Command_Other == 1)
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Attack - OtherInfo.m_Defence).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Attack, OtherInfo.m_Defence);
            }
            else if (Command_Other == 2)
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Attack - OtherInfo.m_Counter).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Attack, OtherInfo.m_Counter);
            }
            else if (Command_Other == 4)
            {
                m_Value.text = "빗나감";
                m_ValueDesc.text = "";
            }
            else
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Attack).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Attack, 0);
            }
            */
        }
        else if (Command_My == 1)   // 방어
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_defence");
            m_Value.text = MyInfo.m_Stat.m_Defence.ToString();
            m_ValueDesc.text = string.Format("{0} + {1}", MyInfo.m_Stat_Card.m_Defence, MyInfo.m_Stat_Medal.m_Defence);
            /*
            m_Value.text = "방어";
            m_ValueDesc.text = "";
            */
        }
        else if (Command_My == 2)   // 카운터
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_counter");
            m_Value.text = MyInfo.m_Stat.m_Counter.ToString();
            m_ValueDesc.text = string.Format("{0} + {1}", MyInfo.m_Stat_Card.m_Counter, MyInfo.m_Stat_Medal.m_Counter);
            /*
            if (Command_Other == 1)
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Counter - OtherInfo.m_Defence).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Counter, OtherInfo.m_Defence);
            }
            else if (Command_Other == 2)
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Counter - OtherInfo.m_Counter).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Counter, OtherInfo.m_Counter);
            }
            else if (Command_Other == 4)
            {
                m_Value.text = "빗나감";
                m_ValueDesc.text = "";
            }
            else
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Counter).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Counter, 0);
            }
            */
        }
        else if (Command_My == 3)   // 스페셜
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_special");
            m_Value.text = MyInfo.m_Stat.m_Special.ToString();
            m_ValueDesc.text = string.Format("{0} + {1}", MyInfo.m_Stat_Card.m_Special, MyInfo.m_Stat_Medal.m_Special);
            /*
            if(Command_Other == 1)
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Special).ToString();
                m_ValueDesc.text = "방어무시";
            }
            else if (Command_Other == 4)
            {
                m_Value.text = "빗나감";
                m_ValueDesc.text = "";
            }
            else
            {
                m_Value.text = Mathf.Max(0, MyInfo.m_Special).ToString();
                m_ValueDesc.text = string.Format("{0} - {1}", MyInfo.m_Special, 0);
            }
            */
        }
        else
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_evade");

            m_Value.text = "회피";
            m_ValueDesc.text = "";
        }

        m_Move.OnReset();
        m_Move.OnStart(0.5f);
    }

    public void Reset()
    {
        m_Move.OnReset();
    }
}
