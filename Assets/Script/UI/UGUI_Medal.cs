﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI_Medal : MonoBehaviour
{
    [ReadOnly]
    public int m_Id;
    [ReadOnly]
    public Dino m_Dino;

    public Image m_Sprite_Dino;
    public UGUI_Kerisdiagramm m_Gauge;

    private bool m_IsSpin = false;
    [ReadOnly]
    public float m_Angle = 0.0f;
    [ReadOnly]
    public float m_AngleRatio = 0.0f;
    [ReadOnly]
    public string m_Desc = "";

    public bool m_ShowTool = false;

    public bool m_Preload = false;

    static public Color Color_Attack = new Color(0.1882353f, 0.09411765f, 0.1607843f, 1.0f);
    static public Color Color_Defence = new Color(0.05490196f, 0.09411765f, 0.2392157f, 1.0f);
    static public Color Color_Counter = new Color(0.04705882f, 0.227451f, 0.2392157f, 1.0f);
    static public Color Color_Evade = new Color(0.1294118f, 0.09803922f, 0.227451f, 1.0f);
    static public Color Color_Special = new Color(0.2235294f, 0.254902f, 0.2156863f, 1.0f);

    private void Awake()
    {
        if (m_Preload)
            TestSet();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Id == 0)
            return;

        if(m_IsSpin)
            transform.Rotate(-Vector3.forward * Time.deltaTime * Random.Range(2000, 3000));

        m_Angle = gameObject.transform.rotation.eulerAngles.z;
        m_AngleRatio = 1.0f - (m_Angle / 360.0f);
        m_Desc = m_Gauge.Win(1.0f - m_AngleRatio);
    }

    public int Get()
    {
        if (m_Desc == "공격")
            return 0;
        else if (m_Desc == "방어")
            return 1;
        else if (m_Desc == "카운터")
            return 2;
        else if (m_Desc == "스페셜")
            return 3;
        else if (m_Desc == "회피")
            return 4;
        else
        {
            // 판정이 이상할 때는 메달 교체하고 로테이션을 0 으로 초기화해줬나 확인 해주자
            Debug.LogErrorFormat("### 메달({0}) 커맨드({1}) 판정 애러 {1}", m_Id, m_Desc);
            return -1;
        }
    }

    public void Set(int DinoId)
    {
        m_Id = DinoId;
        m_Sprite_Dino.sprite = CM_Singleton<GameData>.instance.m_Atlas_UI.GetSprite(string.Format("medal_{0}", DinoId));

        m_Dino = CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[DinoId];
        m_Gauge.Clear();
        if (m_Dino.m_Ratio_Attack > 0)
            //StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Attack, Color_Attack, "공격"));
            CM_Job.Make(m_Gauge.Add_(m_Dino.m_Ratio_Attack, Color_Attack, "공격")).Start();
        if (m_Dino.m_Ratio_Defence > 0)
            //StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Defence, Color_Defence, "방어"));
            CM_Job.Make(m_Gauge.Add_(m_Dino.m_Ratio_Defence, Color_Defence, "방어")).Start();
        if (m_Dino.m_Ratio_Counter > 0)
            //StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Counter, Color_Counter, "카운터"));
            CM_Job.Make(m_Gauge.Add_(m_Dino.m_Ratio_Counter, Color_Counter, "카운터")).Start();
        if (m_Dino.m_Ratio_Special > 0)
            //StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Special, Color_Special, "스페셜"));
            CM_Job.Make(m_Gauge.Add_(m_Dino.m_Ratio_Special, Color_Special, "스페셜")).Start();
        if (m_Dino.m_Ratio_Evade > 0)
            //StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Evade, Color_Evade, "회피"));
            CM_Job.Make(m_Gauge.Add_(m_Dino.m_Ratio_Evade, Color_Evade, "회피")).Start();
    }

    public void Clear()
    {
        m_Gauge.Clear();
    }

    public void OnSpin()
    {
        m_IsSpin = true;
        //StartCoroutine(RotateMedal()); // 이 방식으로 돌리면 battle 쪽 코루틴하고 뭔가 안맞는지 get 할 때 결과가 다른 현상이 있어서 사용 안함
    }

    private IEnumerator RotateMedal()
    {
        while (m_IsSpin)
        {
            yield return YieldHelper.waitForEndOfFrame();
            transform.Rotate(-Vector3.forward * Time.deltaTime * Random.Range(2000,3000));
        }
    }

    public void OffSpin()
    {
        m_IsSpin = false;
    }

    private void OnGUI()
    {
        if (m_ShowTool == false)
            return;

        GUI.Box(new Rect(300, 140, 300, 80), "");
        GUI.Label(new Rect(300, 140, 300, 30), string.Format("z {0} / r {1} / {2}({3})", m_Angle, m_AngleRatio, m_Desc, Get()));

        if (GUI.Button(new Rect(300, 100, 100, 30), "Set"))
            TestSet();
    }

    void TestSet()
    {
        StartCoroutine(m_Gauge.Add_(0.3f, Color_Attack, "공격"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color_Defence, "방어"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color_Counter, "반격"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color_Special, "필살"));
        StartCoroutine(m_Gauge.Add_(0.1f, Color_Evade, "회피"));
    }
}
