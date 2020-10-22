﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    GameData m_GameData;

    // for interface
    [Header("보드")]
    public Transform m_Position_My;         // 공룡 모델 위치
    public SpriteRenderer[] m_Deck_My;
    GameObject m_CurDino_My;
    int m_Hp_My;
    public Image m_Gauge_Hp_My;
    public Text m_Label_Hp_My;
    public Image[] m_Image_Deck_My;
    public Text[] m_Label_Deck_My;
    public UGUI_DinoField m_DinoField_My;
    public UGUI_Medal[] m_Medal_My;
    public Text m_Label_Attack_My;
    public Text m_Label_Defence_My;
    public Text m_Label_Counter_My;
    public Text m_Label_Special_My;

    public Transform m_Position_Other;      // 공룡 모델 위치
    public SpriteRenderer[] m_Deck_Other;
    GameObject m_CurDino_Other;
    int m_Hp_Other;
    public Image m_Gauge_Hp_Other;
    public Text m_Label_Hp_Other;
    public Image[] m_Image_Deck_Other;
    public Text[] m_Label_Deck_Other;
    public UGUI_DinoField m_DinoField_Other;
    public UGUI_Medal[] m_Medal_Other;
    public Text m_Label_Attack_Other;
    public Text m_Label_Defence_Other;
    public Text m_Label_Counter_Other;
    public Text m_Label_Special_Other;

    [Header("전투")]
    public Panel_Blind m_Panel_Blind;
    //public GameObject m_Label_Ready;
    //public GameObject m_Label_Set;
    //public GameObject m_Label_Spin;
    public GameObject m_UI_Counter;
    public Dlg_Command m_Dlg_Command_My;
    public Dlg_Command m_Dlg_Command_Other;

    [Header("카드 픽")]
    public GameObject m_CardTypeSelect;
    public GameObject m_CardPick;
    public Panel_CardPick m_Panel_CardPick;
    public GameObject m_CardMade;

    // main stream
    CM_JobQueue m_JobQueue;

    // for duel
    int m_Turn = 0;
    CM_JobQueue m_JobQueue_Duel;
    int m_Count = 0;


    private void Awake()
    {
        m_GameData = CM_Singleton<GameData>.instance;
        m_GameData.m_MyInfo.Reset_Card();

        if (m_GameData.m_MyInfo.m_Dino.Count == 0)
        {
            // for test
            StartCoroutine(TestLoading());
        }
        else
        {
            SetDeck_Other();
            SetDino();
        }
    }

    IEnumerator TestLoading()
    {
        m_GameData.Add_MyDino(1);
        m_GameData.Add_MyDino(2);
        m_GameData.Add_MyDino(3);

        // 선택한 스테이지로 저장
        m_GameData.m_StageId = 1;

        // 스테이지 정보 겟
        var stage = m_GameData.m_Table_Stage.m_Dic[m_GameData.m_StageId];

        m_GameData.m_OtherInfo.Clear_Dino();
        m_GameData.Add_OtherDino(stage.m_Dino1);
        m_GameData.Add_OtherDino(stage.m_Dino2);
        m_GameData.Add_OtherDino(stage.m_Dino3);

        m_GameData.m_OtherInfo.Reset_Card();
        m_GameData.m_OtherInfo.Set_Card(1, stage.m_Card1);
        m_GameData.m_OtherInfo.Set_Card(2, stage.m_Card2);
        m_GameData.m_OtherInfo.Set_Card(3, stage.m_Card3);
        m_GameData.m_OtherInfo.Set_Card(4, stage.m_Card4);
        m_GameData.m_OtherInfo.Set_Card(5, stage.m_Card5);

        yield return new WaitForSeconds(1.0f);

        SetDeck_Other();
        SetDino();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Turn = 0;

        m_JobQueue = CM_JobQueue.Make()
        //m_GameData.m_JobQueue
            .Enqueue(Step_Ready())
            .Enqueue(Step_Set())
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
        if (m_GameData.m_MyInfo.m_Dino.Count == 0)
            return;

        for (int i = 0; i < m_Medal_My.Length; i++)
        {
            if (i == 0)     // 선봉
            {
                m_CurDino_My = m_GameData.m_MyDino_Object[m_GameData.m_MyInfo.m_Dino[i]];
                m_CurDino_My.transform.parent = null;
                m_CurDino_My.transform.position = m_Position_My.position;
                //m_CurDino_My.transform.localScale = m_Position_My.transform.localScale * 0.0015625f;
                m_CurDino_My.transform.rotation = m_Position_My.rotation;
                m_CurDino_My.transform.parent = m_Position_My;
            }

            if (i < m_GameData.m_MyInfo.m_Dino.Count)
                m_Medal_My[i].Set(m_GameData.m_MyInfo.m_Dino[i]);
            else
                m_Medal_My[i].gameObject.SetActive(false);
        }

        for (int i=0; i<m_Medal_Other.Length; i++)
        {
            if (i == 0)     // 선봉
            {
                m_CurDino_Other = m_GameData.m_OtherDino_Object[m_GameData.m_OtherInfo.m_Dino[i]];
                m_CurDino_Other.transform.parent = null;
                m_CurDino_Other.transform.position = m_Position_Other.position;
                //m_CurDino_Other.transform.localScale = m_Position_Other.transform.localScale * 0.0015625f;
                m_CurDino_Other.transform.rotation = m_Position_Other.rotation;
                m_CurDino_Other.transform.parent = m_Position_Other;
            }

            if(i < m_GameData.m_OtherInfo.m_Dino.Count)
                m_Medal_Other[i].Set(m_GameData.m_OtherInfo.m_Dino[i]);
            else
                m_Medal_Other[i].gameObject.SetActive(false);
        }
    }

    void SetDeck_Other()
    {
        m_Deck_Other[0].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Amulet));
        m_Deck_Other[1].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Ring));
        m_Deck_Other[2].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Glove));
        m_Deck_Other[3].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Map));
        m_Deck_Other[4].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Belt));

        m_Image_Deck_Other[0].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Amulet));
        m_Image_Deck_Other[1].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Ring));
        m_Image_Deck_Other[2].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Glove));
        m_Image_Deck_Other[3].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Map));
        m_Image_Deck_Other[4].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_OtherInfo.m_Card_Belt));

        m_Label_Deck_Other[0].text = string.Format("{0}", m_GameData.m_OtherInfo.m_Attack);
        m_Label_Deck_Other[1].text = string.Format("{0}", m_GameData.m_OtherInfo.m_Defence);
        m_Label_Deck_Other[2].text = string.Format("{0}", m_GameData.m_OtherInfo.m_Counter);
        m_Label_Deck_Other[3].text = string.Format("{0}", m_GameData.m_OtherInfo.m_Special);

        m_Hp_Other = m_GameData.m_OtherInfo.m_Hp;
        m_Label_Hp_Other.text = m_GameData.m_OtherInfo.m_Hp.ToString();

        m_Panel_Blind.Reflash();
    }

    void SetDeck_My()
    {
        m_Deck_My[0].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Amulet));
        m_Deck_My[1].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Ring));
        m_Deck_My[2].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Glove));
        m_Deck_My[3].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Map));
        m_Deck_My[4].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Belt));

        m_Image_Deck_My[0].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Amulet));
        m_Image_Deck_My[1].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Ring));
        m_Image_Deck_My[2].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Glove));
        m_Image_Deck_My[3].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Map));
        m_Image_Deck_My[4].sprite = m_GameData.m_Atlas_Card.GetSprite(string.Format("card_{0}", m_GameData.m_MyInfo.m_Card_Belt));

        m_Label_Deck_My[0].text = string.Format("{0}", m_GameData.m_MyInfo.m_Attack);
        m_Label_Deck_My[1].text = string.Format("{0}", m_GameData.m_MyInfo.m_Defence);
        m_Label_Deck_My[2].text = string.Format("{0}", m_GameData.m_MyInfo.m_Counter);
        m_Label_Deck_My[3].text = string.Format("{0}", m_GameData.m_MyInfo.m_Special);

        m_Hp_My = m_GameData.m_MyInfo.m_Hp;
        m_Label_Hp_My.text = m_GameData.m_MyInfo.m_Hp.ToString();

        m_Panel_Blind.Reflash();
    }




    // https://www.notion.so/UI-798fd7dfb61f4890b9c1a3220a67a6db#834c7b732c204caabc7c95471d3db7d4
    IEnumerator Step_Ready()
    {
        DebugAdd("# 스타트 레디");
        DebugAdd("  아군 등장");
        if(m_CurDino_My)
            m_CurDino_My.GetComponent<DinoObject>().SetAnimation("run");
        m_Position_My.GetComponent<SimpleMove_Lerp_Local>().OnStart(1.0f);

        DebugAdd("  적군 등장");
        if(m_CurDino_Other)
            m_CurDino_Other.GetComponent<DinoObject>().SetAnimation("run");
        m_Position_Other.GetComponent<SimpleMove_Lerp_Local>().OnStart(1.0f);

        yield return new WaitForSeconds(1.0f);

        // 공룡 제자리에-
        if (m_CurDino_My)
            m_CurDino_My.GetComponent<DinoObject>().SetAnimation("idle");
        m_CurDino_My.GetComponent<DinoObject>().SetTarget(m_CurDino_Other.transform);
        if (m_CurDino_Other)
            m_CurDino_Other.GetComponent<DinoObject>().SetAnimation("idle");
        m_CurDino_My.GetComponent<DinoObject>().SetTarget(m_CurDino_My.transform);

        yield return new WaitForSeconds(2.0f);

        DebugAdd("# 엔드 레디");
    }



    List<int> m_List_Deck;

    IEnumerator Step_Set()
    {
        DebugAdd("# 스타트 셋 (덱 픽)");

        m_CardTypeSelect.SetActive(true);

        m_JobQueue.Pause();

        yield return new WaitForEndOfFrame();

        while (m_JobQueue.running == false)
        {
            yield return null;
        }

        DebugAdd("# 엔드 셋");
    }

    void SetCardPick(int Type)
    {
        m_List_Deck = m_GameData.m_Table_Deck.GetShuffledDeck(Type);
        m_CardPick.SetActive(true);
        m_CardTypeSelect.SetActive(false);
        m_Panel_CardPick.SetPickCards(m_List_Deck);
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
            //DebugAdd("승패 체크");
            if (m_Turn == 10)
                break;

            yield return new WaitForSeconds(1.0f);
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

    const int CONST_MAX_COUNT = 3;

    IEnumerator Duel_WaitInput()
    {
        DebugAdd("# 입력 다이노 필드 스핀 시작");

        m_Panel_Blind.gameObject.SetActive(true);
        m_DinoField_My.StartSpin();
        m_DinoField_Other.StartSpin();

        m_Count = CONST_MAX_COUNT;
        CM_Job.Make(Duel_Counting())
                .Repeat(CONST_MAX_COUNT)
                .NotifyOnJobComplete((object sender, CM_JobEventArgs e) => {
                    DebugAdd(string.Format("input Count {0}", m_Count));
                }).Start();

        while (true)
        {
            if (m_Count == 0)
                break;

            yield return null;
        }
        
        m_UI_Counter.SetActive(false);
        m_DinoField_My.StopSpin();
        m_DinoField_Other.StopSpin();

        // 판정 연출
        m_Dlg_Command_My.Set(m_DinoField_My.GetCommand(), m_DinoField_Other.GetCommand());
        m_Dlg_Command_Other.Set(m_DinoField_Other.GetCommand(), m_DinoField_My.GetCommand());

        yield return new WaitForSeconds(2.0f);  // 판정 연출 끝 대기

        m_Dlg_Command_My.Reset();
        m_Dlg_Command_Other.Reset();

        CommandCalc(m_DinoField_My.GetCommand(), m_DinoField_Other.GetCommand());

        m_Panel_Blind.gameObject.SetActive(false);

        DebugAdd("# 입력 대기 종료");
    }

    void CommandCalc(int Command_My, int Command_Other)
    {
        // my
        if (Command_My == 0) // 공격
        {
            TakeDamage_Other(m_GameData.m_MyInfo.m_Attack - m_GameData.m_OtherInfo.m_Defence);
        }
        else if (Command_My == 1)   // 방어
        {
            
        }
        else if (Command_My == 2)   // 카운터
        {
            TakeDamage_Other(m_GameData.m_MyInfo.m_Counter - m_GameData.m_OtherInfo.m_Defence);
        }
        else if (Command_My == 3)   // 스페셜
        {
            TakeDamage_Other(m_GameData.m_MyInfo.m_Special);
        }
        else
        {
            
        }

        // other
        if (Command_Other == 0) // 공격
        {
            TakeDamage_My(m_GameData.m_OtherInfo.m_Attack - m_GameData.m_MyInfo.m_Defence);
        }
        else if (Command_Other == 1)   // 방어
        {

        }
        else if (Command_Other == 2)   // 카운터
        {
            TakeDamage_My(m_GameData.m_OtherInfo.m_Counter - m_GameData.m_MyInfo.m_Defence);
        }
        else if (Command_Other == 3)   // 스페셜
        {
            TakeDamage_My(m_GameData.m_OtherInfo.m_Special);
        }
        else
        {

        }
    }

    void TakeDamage_My(int Damage)
    {
        DebugAdd(string.Format("받은 데미지 {0}", Mathf.Max(0, Damage)));

        m_Hp_My = Mathf.Max(0, m_Hp_My - Damage);
        m_Gauge_Hp_My.fillAmount = m_Hp_My / (float)m_GameData.m_MyInfo.m_Hp;
        m_Label_Hp_My.text = m_Hp_My.ToString();
    }

    void TakeDamage_Other(int Damage)
    {
        DebugAdd(string.Format("준 데미지 {0}", Mathf.Max(0, Damage)));

        m_Hp_Other = Mathf.Max(0, m_Hp_Other - Mathf.Max(0, Damage));
        m_Gauge_Hp_Other.fillAmount = m_Hp_Other / (float)m_GameData.m_OtherInfo.m_Hp;
        m_Label_Hp_Other.text = m_Hp_Other.ToString();
    }

    private IEnumerator Duel_Counting()
    {
        yield return new WaitForSeconds(1.0f);

        --m_Count;

        m_UI_Counter.SetActive(true);
        m_UI_Counter.GetComponent<Text>().text = m_Count.ToString();
    }


    IEnumerator Duel_Fight()
    {
        DebugAdd("# 스타트 전투 연출");

        if (m_CurDino_My)
            m_CurDino_My.GetComponent<DinoObject>().SetAnimation("attack_1");

        if (m_CurDino_Other)
            m_CurDino_Other.GetComponent<DinoObject>().SetAnimation("attack_1");

        yield return new WaitForSeconds(1.0f);

        if (m_CurDino_My)
            m_CurDino_My.GetComponent<DinoObject>().SetAnimation("idle");

        if (m_CurDino_Other)
            m_CurDino_Other.GetComponent<DinoObject>().SetAnimation("idle");

        yield return new WaitForSeconds(3.0f);
        DebugAdd("# 엔드 전투 연출");
    }

    IEnumerator Duel_End()
    {
        yield return new WaitForSeconds(1.0f);
        DebugAdd("# 엔드 턴");
    }

    #endregion





    #region for Msg

    public void CardTypeSelect_Attack()
    {
        SetCardPick(1);
    }

    public void CardTypeSelect_Defence()
    {
        SetCardPick(2);
    }

    public void CardTypeSelect_Counter()
    {
        SetCardPick(3);
    }

    public void CardTypeSelect_Special()
    {
        SetCardPick(4);
    }

    public void CardMade()
    {
        SetDeck_My();

        m_CardMade.SetActive(false);
        m_JobQueue.Resume();
    }

    public void StartSpin()
    {

    }

    public void StopSpin()
    {

    }

    #endregion




    #region for Debug

    [Header("디버그")]
    public bool m_bShowLog = true;
    protected ArrayList m_sDebug = new ArrayList();
    protected void DebugAdd(string s)
    {
        if (m_bShowLog)
            m_sDebug.Add(s);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 100, 30), "Move"))
            m_Position_My.GetComponent<SimpleMove_Lerp_Local>().OnStart(1.0f);

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
