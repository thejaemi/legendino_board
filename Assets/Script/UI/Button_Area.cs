using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Area : MonoBehaviour
{
    public UILabel m_Label_Title;
    public UIButton m_Button;
    public UISprite m_Sprite_Icon;
    public GameObject m_Lock;
    public GameObject m_NextArrow;

    
    public void Init(int Id)
    {
        gameObject.name = Id.ToString();
        m_Label_Title.text = CM_Singleton<GameData>.instance.m_Table_Stage.m_Dic[Id].m_StageName;
        m_Sprite_Icon.spriteName = string.Format("img_etc_{0}", CM_Singleton<GameData>.instance.m_Table_Stage.m_Dic[Id].m_StageImage);
        if (CM_Singleton<GameData>.instance.m_Stage_Step + 1 < Id)
        {
            m_Button.isEnabled = false;
            m_Lock.SetActive(true);
        }
        else
        {
            m_Button.isEnabled = true;
            m_Lock.SetActive(false);
        }

        if (Id == CM_Singleton<GameData>.instance.m_Table_Stage.m_Dic.Count)
            m_NextArrow.SetActive(false);
        else
            m_NextArrow.SetActive(true);
    }

    public void OnPush()
    {
        int Id = int.Parse(UIButton.current.name);

        GameObject.Find("Main").GetComponent<Lobby>().OnShow_Info(Id);
    }
}
