using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanScene : MonoBehaviour
{
    public GameObject buttonOfMedalInput;
    public GameObject buttonOfBattleScene;
    public GameObject[] medals;

    void Start()
    {
        CheckMedals();
    }

    private void CheckMedals()
    {
        for(int i = 0; i < medals.Length; i++)
        {
            if(medals[i].activeInHierarchy == false)
            {
                buttonOfMedalInput.SetActive(true);
                return;
            }
        }
        buttonOfMedalInput.SetActive(false);
        buttonOfBattleScene.SetActive(true);
    }

    public void OnClickMedalQRCodeInput()
    {
        for(int i = 0; i < medals.Length; i++)
        {
            if(medals[i].activeInHierarchy == false)
            {
                medals[i].SetActive(true);
                break;
            }
        }

        CheckMedals();
    }
    
    public void OnClickBattleButton()
    {
        SceneManager.LoadScene("Battle");
    }


    public void Test()
    {
        Debug.Log("Test");
        CM_Singleton<GameData>.instance.Test_Add_MyDino();

        Invoke("GotoBattle", 2.0f);
    }

    public void GotoBattle()
    {
        SceneManager.LoadScene("Battle");
    }

/*
    #region for test

    enum EMode
    {
        Default = 0,
        SelectDino,
        SelectDinoOther,
    };

    EMode m_CurMode = EMode.Default;
    int m_StartX = 30;
    int m_StartY = 30;
    string m_sTmp = "";

    private void OnGUI()
    {
        if (m_CurMode == EMode.Default)
        {
            GUI.BeginGroup(new Rect(320, 30, 400, 400), "내 공룡");
            if (GUI.Button(new Rect(0, 20, 140, 60), "공룡 추가"))
                m_CurMode = EMode.SelectDino;

            if (GUI.Button(new Rect(160, 20, 140, 60), "초기화"))
                CM_Singleton<GameData>.instance.Clear_MyDino();

            // 선택된 공룡
            m_sTmp = "";
            for (int i = 0; i < CM_Singleton<GameData>.instance.m_MyInfo.m_Dino.Count; i++)
                m_sTmp = string.Format("{0} {1}", m_sTmp, CM_Singleton<GameData>.instance.m_MyInfo.m_Dino[i]);
            GUI.Label(new Rect(180, 25, 200, 60), m_sTmp);
            GUI.EndGroup();



            GUI.BeginGroup(new Rect(320, 130, 400, 400), "상대 공룡");
            if (GUI.Button(new Rect(0, 20, 140, 60), "공룡 추가"))
                m_CurMode = EMode.SelectDinoOther;

            if (GUI.Button(new Rect(160, 20, 140, 60), "초기화"))
                CM_Singleton<GameData>.instance.Clear_OtherDino();

            // 선택된 공룡
            m_sTmp = "";
            for (int i = 0; i < CM_Singleton<GameData>.instance.m_OtherInfo.m_Dino.Count; i++)
                m_sTmp = string.Format("{0} {1}", m_sTmp, CM_Singleton<GameData>.instance.m_OtherInfo.m_Dino[i]);
            GUI.Label(new Rect(180, 25, 100, 30), m_sTmp);
            GUI.EndGroup();




            if (GUI.Button(new Rect(320, 230, 200, 60), "임시배틀"))
            {
                CM_Singleton<GameData>.instance.m_StageId = 1;
                SceneManager.LoadScene("Loading_Battle");
            }
        }
        else if (m_CurMode == EMode.SelectDino || m_CurMode == EMode.SelectDinoOther)
        {
            int sx = 0;
            int sy = 0;

            for (int i = 0; i < 200; i++)
            {
                if (i % 10 == 0)
                    sx = 0;

                sy = i / 10 * 60;
                if (GUI.Button(new Rect(m_StartX + sx, m_StartY + sy, 60, 60), i.ToString()))
                {
                    Debug.LogFormat("Push {0}", i);
                    if (m_CurMode == EMode.SelectDino)
                        CM_Singleton<GameData>.instance.Add_MyDino(i);
                    else if (m_CurMode == EMode.SelectDinoOther)
                        CM_Singleton<GameData>.instance.Add_OtherDino(i);
                    m_CurMode = EMode.Default;
                }
                sx += 60;
            }
        }
    }

    #endregion
*/
}
