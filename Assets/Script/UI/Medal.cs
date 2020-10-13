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

    static public Color Color_Attack = new Color(0.1882353f, 0.09411765f, 0.1607843f);
    static public Color Color_Defence = new Color(0.05490196f, 0.09411765f, 0.2392157f);
    static public Color Color_Counter = new Color(0.04705882f, 0.227451f, 0.2392157f);
    static public Color Color_Evade = new Color(0.1294118f, 0.09803922f, 0.227451f);
    static public Color Color_Special = new Color(0.2235294f, 0.254902f, 0.2156863f);

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
        m_Sprite_Dino.spriteName = string.Format("medal_{0}", DinoId);

        m_Dino = CM_Singleton<GameData>.instance.m_Table_Dino.m_Dic[DinoId];
        if(m_Dino.m_Ratio_Attack > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Attack, Color_Attack, "공격"));
        if (m_Dino.m_Ratio_Defence > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Defence, Color_Defence, "방어"));
        if (m_Dino.m_Ratio_Counter > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Counter, Color_Counter, "카운터"));
        if (m_Dino.m_Ratio_Special > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Special, Color_Special, "스페셜"));
        if (m_Dino.m_Ratio_Evade > 0)
            StartCoroutine(m_Gauge.Add_(m_Dino.m_Ratio_Evade, Color_Evade, "회피"));
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
        StartCoroutine(m_Gauge.Add_(0.3f, Color_Attack, "공격"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color_Defence, "방어"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color_Counter, "반격"));
        StartCoroutine(m_Gauge.Add_(0.2f, Color_Special, "필살"));
        StartCoroutine(m_Gauge.Add_(0.1f, Color_Evade, "회피"));
    }
}
