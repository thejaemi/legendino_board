using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_A : MonoBehaviour
{
    CM_JobQueue m_JobQueue;

    enum EMode
    {
        Default = 0,
        SelectDino,
        SelectDinoOther,
    };

    EMode m_CurMode = EMode.Default;
    int m_StartX = 30;
    int m_StartY = 30;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(CM_Dispatcher.instance);
        //m_JobQueue = CM_JobQueue.Make()
            //.Enqueue(Job())
            //.Enqueue(Tick())
            //.Repeat()
            //.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Tick()
    {
        Debug.Log("Tick");
        yield return null;
    }

    IEnumerator Job()
    {
        Debug.Log("Job Start");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Job End");
    }

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(100, 100, 100, 100), "SceneMove"))
    //        SceneManager.LoadScene("Test_B");
    //}

    string m_sTmp = "";

    private void OnGUI()
    {
        if (m_CurMode == EMode.Default)
        {
            GUI.BeginGroup(new Rect(320, 30, 400, 400), "내 공룡");
            if (GUI.Button(new Rect(0, 20, 140, 60), "공룡 추가"))
                m_CurMode = EMode.SelectDino;

            if (GUI.Button(new Rect(160, 20, 140, 60), "초기화"))
                CM_Singleton<GameData>.instance.m_MyInfo.Clear_Dino();

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
                CM_Singleton<GameData>.instance.m_MyInfo.Clear_Dino();

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
/*
            float sy = Screen.height - 600;

            GUI.BeginGroup(new Rect(0, sy, 300, 600));

            float y = 0f;
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attack_1"))
                m_MyDino.SetAnimation("attack_1");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attack_2"))
                m_MyDino.SetAnimation("attack_2");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attack_3"))
                m_MyDino.SetAnimation("attack_3");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attack_4"))
                m_MyDino.SetAnimation("attack_4");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attackready_1"))
                m_MyDino.SetAnimation("attackready_1");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attackready_2"))
                m_MyDino.SetAnimation("attackready_2");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attackready_3"))
                m_MyDino.SetAnimation("attackready_3");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "attackready_4"))
                m_MyDino.SetAnimation("attackready_4");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "damage"))
                m_MyDino.SetAnimation("damage");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "defense"))
                m_MyDino.SetAnimation("defense");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "die"))
                m_MyDino.SetAnimation("die");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "dodge"))
                m_MyDino.SetAnimation("dodge");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "idle"))
                m_MyDino.SetAnimation("idle");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "natk_1"))
                m_MyDino.SetAnimation("natk_1");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "natkready_1"))
                m_MyDino.SetAnimation("natkready_1");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "run"))
                m_MyDino.SetAnimation("run");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "soul"))
                m_MyDino.SetAnimation("soul");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "special_1"))
                m_MyDino.SetAnimation("special_1");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "special_2"))
                m_MyDino.SetAnimation("special_2");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "specialready_1"))
                m_MyDino.SetAnimation("specialready_1");
            if (GUI.Button(new Rect(0, y += 30, 100, 30), "specialready_2"))
                m_MyDino.SetAnimation("specialready_2");

            GUI.EndGroup();
*/
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
}
