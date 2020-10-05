using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLobby : MonoBehaviour
{
    public DinoObject m_MyDino;
    public DinoObject m_OtherDino;
    


    enum EMode
    {
        Default = 0,
        SelectDino,
        SelectDinoOther,
    };

    EMode m_CurMode = EMode.Default;
    int m_StartX = 522;
    int m_StartY = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (m_MyDino && m_OtherDino)
        {
            m_MyDino.gameObject.layer = 0;
            m_OtherDino.gameObject.layer = 0;
            m_MyDino.SetTarget(m_OtherDino.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string m_sTmp = "";

    private void OnGUI()
    {
        if(m_CurMode == EMode.Default)
        {
            GUI.BeginGroup(new Rect(520, 30, 200, 200), "내 공룡");
            if (GUI.Button(new Rect(0, 20, 70, 30), "공룡 추가"))
                m_CurMode = EMode.SelectDino;

            if (GUI.Button(new Rect(80, 20, 70, 30), "초기화"))
                CM_Singleton<GameData>.instance.Clear_MyDino();

            // 선택된 공룡
            m_sTmp = "";
            for (int i = 0; i < CM_Singleton<GameData>.instance.m_MyDino.Count; i++)
                m_sTmp = string.Format("{0} {1}", m_sTmp, CM_Singleton<GameData>.instance.m_MyDino[i]);
            GUI.Label(new Rect(180, 25, 100, 30), m_sTmp);
            GUI.EndGroup();



            GUI.BeginGroup(new Rect(520, 100, 200, 200), "상대 공룡");
            if (GUI.Button(new Rect(0, 20, 70, 30), "공룡 추가"))
                m_CurMode = EMode.SelectDinoOther;

            if (GUI.Button(new Rect(80, 20, 70, 30), "초기화"))
                CM_Singleton<GameData>.instance.Clear_MyDino();

            // 선택된 공룡
            m_sTmp = "";
            for (int i = 0; i < CM_Singleton<GameData>.instance.m_OtherDino.Count; i++)
                m_sTmp = string.Format("{0} {1}", m_sTmp, CM_Singleton<GameData>.instance.m_OtherDino[i]);
            GUI.Label(new Rect(180, 25, 100, 30), m_sTmp);
            GUI.EndGroup();




            if (GUI.Button(new Rect(522, 180, 100, 30), "임시배틀"))
            {
                CM_Singleton<GameData>.instance.m_StageId = 1;
                SceneManager.LoadScene("Loading_Battle");
            }

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
        }
        else if(m_CurMode == EMode.SelectDino || m_CurMode == EMode.SelectDinoOther)
        {
            int sx = 0;
            int sy = 0;

            /*
            for(int i=0; i<200; i++)
            {
                if (i % 40 == 0)
                    sx = 0;

                sy = i / 40 * 30;
                if(GUI.Button(new Rect(30 + sx, 30 + sy, 30, 30), i.ToString()))
                    CM_Singleton<GameData>.instance.Add_MyDino(i);
                sx += 30;
            }
            */


            for (int i = 0; i < 200; i++)
            {
                if (i % 10 == 0)
                    sx = 0;

                sy = i / 10 * 30;
                if (GUI.Button(new Rect(m_StartX + sx, m_StartY + sy, 30, 30), i.ToString()))
                {
                    if(m_CurMode == EMode.SelectDino)
                        CM_Singleton<GameData>.instance.Add_MyDino(i);
                    else if(m_CurMode == EMode.SelectDinoOther)
                        CM_Singleton<GameData>.instance.Add_OtherDino(i);
                    m_CurMode = EMode.Default;
                }
                sx += 30;
            }
        }
    }
}
