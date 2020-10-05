using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal : MonoBehaviour
{
    Dino m_Dino;

    public UISprite m_Sprite_Dino;
    public UI_Kerisdiagramm m_Gauge;

    private bool m_IsSpin = false;
    public float m_Angle = 0.0f;
    public float m_AngleRatio = 0.0f;
    string m_Desc = "";

    public bool m_ShowTool = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Angle = gameObject.transform.rotation.eulerAngles.z;
        m_AngleRatio = 1.0f - (m_Angle / 360.0f);
        m_Desc = m_Gauge.Win(1.0f - m_AngleRatio);
    }

    public void Set(int DinoId)
    {
        m_Sprite_Dino.spriteName = string.Format("dino_{0}", 168);

        m_Dino = CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[DinoId];
        if(m_Dino.m_Ratio_Attack > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Attack, Color.red, "공격"));
        if (m_Dino.m_Ratio_Defence > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Defence, Color.blue, "방어"));
        if (m_Dino.m_Ratio_Counter > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Counter, Color.cyan, "카운터"));
        if (m_Dino.m_Ratio_Special > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Special, Color.yellow, "스페셜"));
        if (m_Dino.m_Ratio_Evade > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Evade, Color.green, "회피"));
    }

    public void OnSpin()
    {
        m_IsSpin = true;
        StartCoroutine(RotateMedal());
    }

    private IEnumerator RotateMedal()
    {
        while (m_IsSpin)
        {
            yield return YieldHelper.waitForEndOfFrame();
            transform.Rotate(-Vector3.forward * Time.deltaTime * 3000);
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

        GUI.Label(new Rect(550, 140, 300, 30), string.Format("z {0} / r {1} / {2}", m_Angle, m_AngleRatio, m_Desc));

        if (GUI.Button(new Rect(550, 100, 100, 30), "Set"))
            TestSet();
    }

    void TestSet()
    {
        StartCoroutine(m_Gauge.Add_(0.3f, Color.red, "공격"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color.blue, "방어"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color.cyan, "반격"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color.yellow, "필살"));
        StartCoroutine(m_Gauge.Add_(0.1f, Color.green, "회피"));
    }
}
