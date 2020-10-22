using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel_StageInfo : MonoBehaviour
{
    public UISprite m_Sprite_Icon;
    public UILabel m_Label_Local;
    public UILabel m_Label_Name;
    public Medal[] m_Medals;
    public UILabel m_Label_Attack;
    public UILabel m_Label_Defence;
    public UILabel m_Label_Counter;
    public UILabel m_Label_Special;
    public UILabel m_Label_Hp;

    public Panel_MedalView m_MedalView;

    public void Set(int Id)
    {
        // 선택한 스테이지로 저장
        CM_Singleton<GameData>.instance.m_StageId = Id;

        // 스테이지 정보 겟
        var stage = CM_Singleton<GameData>.instance.m_Table_Stage.m_Dic[Id];

        m_Label_Local.text = string.Format("지역{0}", stage.m_Id);
        m_Label_Name.text = stage.m_StageName;
        m_Sprite_Icon.spriteName = string.Format("img_etc_{0}", stage.m_StageImage);

        if (stage.m_Dino1 > 0)
        {
            m_Medals[0].gameObject.SetActive(true);
            m_Medals[0].Set(stage.m_Dino1);
        }
        else
            m_Medals[0].gameObject.SetActive(false);

        if (stage.m_Dino2 > 0)
        {
            m_Medals[1].gameObject.SetActive(true);
            m_Medals[1].Set(stage.m_Dino2);
        }
        else
            m_Medals[1].gameObject.SetActive(false);

        if (stage.m_Dino3 > 0)
        {
            m_Medals[2].gameObject.SetActive(true);
            m_Medals[2].Set(stage.m_Dino3);
        }
        else
            m_Medals[2].gameObject.SetActive(false);


        CM_Singleton<GameData>.instance.m_OtherInfo.Clear_Dino();
        CM_Singleton<GameData>.instance.Add_OtherDino(stage.m_Dino1);
        CM_Singleton<GameData>.instance.Add_OtherDino(stage.m_Dino2);
        CM_Singleton<GameData>.instance.Add_OtherDino(stage.m_Dino3);


        CM_Singleton<GameData>.instance.m_OtherInfo.Reset_Card();
        CM_Singleton<GameData>.instance.m_OtherInfo.Set_Card(1, stage.m_Card1);
        CM_Singleton<GameData>.instance.m_OtherInfo.Set_Card(2, stage.m_Card2);
        CM_Singleton<GameData>.instance.m_OtherInfo.Set_Card(3, stage.m_Card3);
        CM_Singleton<GameData>.instance.m_OtherInfo.Set_Card(4, stage.m_Card4);
        CM_Singleton<GameData>.instance.m_OtherInfo.Set_Card(5, stage.m_Card5);

        m_Label_Attack.text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Attack.ToString();
        m_Label_Defence.text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Defence.ToString();
        m_Label_Counter.text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Counter.ToString();
        m_Label_Special.text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Special.ToString();
        m_Label_Hp.text = CM_Singleton<GameData>.instance.m_OtherInfo.m_Hp.ToString();

    }



    public void OnBattle()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene("Test_A");
#else
        SceneManager.LoadScene("Scan");
#endif
    }

    public void OnClose_Info()
    {
        gameObject.SetActive(false);
    }

    public void OnShow_DetailInfo()
    {
        m_MedalView.gameObject.SetActive(true);
        m_MedalView.Set(UIButton.current.gameObject.transform.parent.GetComponent<Medal>().m_Id);
    }
}
