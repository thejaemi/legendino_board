using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class Scan : MonoBehaviour
{
    public GameObject m_Panel_Scan;
    public QRCamScreen m_QRMgr;
    public SkeletonAnimation m_QRSymbol;
    public UGUI_Medal[] m_Medals;
    public GameObject[] m_Effects;
    public GameObject m_Button;
    List<string> m_ScanList = new List<string>();

    private void Awake()
    {
#if UNITY_EDITOR
        m_Panel_Scan.SetActive(false);
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScanDino(string Code, int DinoId)
    {
        m_QRSymbol.gameObject.SetActive(false);
        m_QRSymbol.AnimationName = "snap";
        m_QRSymbol.gameObject.SetActive(true);

        if(Add_Medal(Code, DinoId))
            CM_Singleton<GameData>.instance.Add_MyDino(DinoId);

        ResetQRSymbol();

        if (m_ScanList.Count == 3)
        {
#if !UNITY_EDITOR
            if(m_QRMgr)
                m_QRMgr.Stop();
#endif
            m_Button.SetActive(true);
            m_QRSymbol.gameObject.SetActive(false);
        }
    }

    void ResetQRSymbol()
    {
#if !UNITY_EDITOR
        if (m_QRMgr)
            m_QRMgr.StartDecode();
#endif
        m_QRSymbol.gameObject.SetActive(false);
        m_QRSymbol.AnimationName = "start";
        m_QRSymbol.gameObject.SetActive(true);
    }

    bool Add_Medal(string Code, int DinoId)
    {
        if(m_ScanList.Contains(Code))
        {
            return false;
        }

        for(int i=0; i<m_Medals.Length; i++)
        {
            if(m_Medals[i].gameObject.activeSelf == false)
            {
                SoundManager.PlaySFX(gameObject, "fx_u_0014");
                m_ScanList.Add(Code);
                m_Medals[i].Set(DinoId);
                m_Medals[i].gameObject.SetActive(true);
                m_Effects[i].gameObject.SetActive(true);
                return true;
            }
        }

        return false;
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Loading_Battle");
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 100, 50), "Test_1"))
            ScanDino("Test1", 1);

        if (GUI.Button(new Rect(50, 100, 100, 50), "Test_2"))
            ScanDino("Test2", 2);

        if (GUI.Button(new Rect(50, 150, 100, 50), "Test_3"))
            ScanDino("Test3", 3);

        if (GUI.Button(new Rect(50, 200, 100, 50), "Reset"))
            ResetQRSymbol();
    }

#endif
}
