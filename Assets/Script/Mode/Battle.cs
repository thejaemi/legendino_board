using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    GameData m_GameData;

    // for interface
    public Transform m_Position_My;         // 공룡 모델 위치
    public DinoField m_DinoField_My;
    public Medal[] m_Medal_My;
    public Transform m_Position_Other;      // 공룡 모델 위치
    public DinoField m_DinoField_Other;
    public Medal[] m_Medal_Other;

    GameObject m_CurDino_My;
    GameObject m_CurDino_Other;

    // for duel
    int m_Turn = 0;
    CM_JobQueue m_JobQueue_Duel;
    int m_Count = 0;


    private void Awake()
    {
        m_GameData = CM_Singleton<GameData>.instance;

        SetDino();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Turn = 0;

        //CM_JobQueue.Make()
        m_GameData.m_JobQueue
            .Enqueue(Step_Ready())
            .Enqueue(Step_Go())
            .Enqueue(Step_Duel())
            .Enqueue(Step_Result())
            .Enqueue(Step_End())
            .Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDino()
    {
        for (int i = 0; i < m_Medal_My.Length; i++)
        {
            if (i == 0)     // 선봉
            {
                GameObject obj = m_GameData.m_MyDino_Object[m_GameData.MyDino[i]];
                obj.transform.parent = null;
                obj.transform.position = m_Position_My.position;
                obj.transform.localScale = m_Position_My.transform.localScale * 0.0015625f;
                obj.transform.rotation = m_Position_My.rotation;
                obj.transform.parent = m_Position_My;
            }

            if (i < m_GameData.MyDino.Count)
                m_Medal_My[i].Set(m_GameData.MyDino[i]);
            else
                m_Medal_My[i].gameObject.SetActive(false);
        }

        for(int i=0; i<m_Medal_Other.Length; i++)
        {
            if (i == 0)     // 선봉
            {
                GameObject obj = m_GameData.m_OtherDino_Object[m_GameData.OtherDino[i]];
                obj.transform.parent = null;
                obj.transform.position = m_Position_Other.position;
                obj.transform.localScale = m_Position_Other.transform.localScale * 0.0015625f;
                obj.transform.rotation = m_Position_Other.rotation;
                obj.transform.parent = m_Position_Other;
            }

            if(i < m_GameData.OtherDino.Count)
                m_Medal_Other[i].Set(m_GameData.OtherDino[i]);
            else
                m_Medal_Other[i].gameObject.SetActive(false);
        }
    }


    IEnumerator Step_Ready()
    {
        DebugAdd("# 스타트 레디");
        DebugAdd("아군 등장");
        DebugAdd("적군 등장");

        yield return new WaitForSeconds(1.0f);

        DebugAdd("# 엔드 레디");
    }

    IEnumerator Step_Go()
    {
        DebugAdd("# 스타트 고");
        DebugAdd("아군 파이팅 애니");
        DebugAdd("적군 파이팅 애니");
        DebugAdd("아군 선 패시브");
        DebugAdd("적군 선 패시브");

        yield return new WaitForSeconds(1.0f);

        DebugAdd("아군 후 패시브");
        DebugAdd("적군 후 패시브");
        DebugAdd("# 엔드 고");
    }

    IEnumerator Step_Duel()
    {
        m_JobQueue_Duel = CM_JobQueue.Make()
        .Enqueue(Duel_Start())
        .Enqueue(Duel_WaitInput())
        .Enqueue(Duel_Fight())
        .Enqueue(Duel_End())
        .Repeat()
        .Start();

        while (true)
        {
            DebugAdd("승패 체크");
            if (m_Turn == 10)
                break;

            yield return null;
        }

        m_JobQueue_Duel.KillAll();
    }

    IEnumerator Step_Result()
    {
        DebugAdd("# 스타트 결과");

        yield return new WaitForSeconds(1.0f);

        DebugAdd("# 엔드 결과");
    }

    IEnumerator Step_End()
    {
        DebugAdd("# 엔드");

        yield break;
    }

    #region for Duel

    IEnumerator Duel_Start()
    {
        ++m_Turn;
        DebugAdd(string.Format("# {0} 턴 스타트", m_Turn));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Duel_WaitInput()
    {
        DebugAdd("# 입력 대기 시작");

        m_Count = 0;
        CM_Job.Make(Duel_Counting())
                .Repeat(10)
                .NotifyOnJobComplete((object sender, CM_JobEventArgs e) => {
                    DebugAdd(string.Format("input Count {0}", m_Count));
                }).Start();

        while (true)
        {
            if (m_Count == 10)
                break;

            yield return null;
        }

        DebugAdd("# 입력 대기 종료");
    }

    private IEnumerator Duel_Counting()
    {
        yield return new WaitForSeconds(1.0f);

        ++m_Count;
        //DebugAdd(string.Format("input Count {0}", m_Count));
    }


    IEnumerator Duel_Fight()
    {
        DebugAdd("# 스타트 전투 연출");
        yield return new WaitForSeconds(1.0f);
        DebugAdd("# 엔드 전투 연출");
    }

    IEnumerator Duel_End()
    {
        yield return new WaitForSeconds(1.0f);
        DebugAdd("# 엔드 턴");
    }

    #endregion



    #region for Debug

    public bool m_bShowLog = true;
    protected ArrayList m_sDebug = new ArrayList();
    protected void DebugAdd(string s)
    {
        if (m_bShowLog)
            m_sDebug.Add(s);
    }

    void OnGUI()
    {
        if (m_bShowLog)
        {
            GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 16;
            int nScrW = Screen.width;
            int nScrH = Screen.height;
            GUI.BeginGroup(new Rect(0, nScrH - 500, nScrW, 500));
            GUI.Box(new Rect(0, 0, 300, 500), "");
            if (m_sDebug.Count > 25)
                m_sDebug.RemoveAt(0);
            for (int i = 0; i < m_sDebug.Count; i++)
                GUI.Label(new Rect(0, i * 20.0f, nScrW, 20.0f), (string)m_sDebug[i]);
            GUI.EndGroup();
        }
    }

    #endregion
}
