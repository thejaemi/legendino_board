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

    public void Set(int Command_My, int Command_Other)
    {
        if(Command_My == 0) // 공격
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_attack");
            m_Value.text = Mathf.Max(0, CM_Singleton<GameData>.instance.m_MyInfo.m_Attack - CM_Singleton<GameData>.instance.m_OtherInfo.m_Defence).ToString();
            m_ValueDesc.text = string.Format("({0} - {1})", CM_Singleton<GameData>.instance.m_MyInfo.m_Attack, CM_Singleton<GameData>.instance.m_OtherInfo.m_Defence);
        }
        else if (Command_My == 1)   // 방어
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_defence");
            m_Value.text = Mathf.Max(0, CM_Singleton<GameData>.instance.m_MyInfo.m_Defence - CM_Singleton<GameData>.instance.m_OtherInfo.m_Attack).ToString();
            m_ValueDesc.text = string.Format("({0} - {1})", CM_Singleton<GameData>.instance.m_MyInfo.m_Defence, CM_Singleton<GameData>.instance.m_OtherInfo.m_Attack);
        }
        else if (Command_My == 2)   // 카운터
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_counter");
            m_Value.text = string.Format("{0}, {1}", Mathf.Max(0, CM_Singleton<GameData>.instance.m_MyInfo.m_Attack - CM_Singleton<GameData>.instance.m_OtherInfo.m_Defence),
                Mathf.Max(0, CM_Singleton<GameData>.instance.m_MyInfo.m_Defence - CM_Singleton<GameData>.instance.m_OtherInfo.m_Attack));
            m_ValueDesc.text = "카운터";
        }
        else if (Command_My == 3)   // 스페셜
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_special");
            m_Value.text = CM_Singleton<GameData>.instance.m_MyInfo.m_Special.ToString();
            m_ValueDesc.text = string.Format("({0} - 0", CM_Singleton<GameData>.instance.m_MyInfo.m_Special);
        }
        else
        {
            m_Image_Mark.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite("icon_evade");
            m_Value.text = "0";
            m_ValueDesc.text = "회피";
        }


        m_Move.OnReset();
        m_Move.OnStart(0.5f);
    }

    public void Reset()
    {
        m_Move.OnReset();
    }
}
