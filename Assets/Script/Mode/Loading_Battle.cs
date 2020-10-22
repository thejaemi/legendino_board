using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Loading_Battle : MonoBehaviour
{
    public Image m_Image_Icon;
    public Text m_Label_Area;
    public Text m_Label_Name;

    public UGUI_Medal[] m_Medals_My;
    public UGUI_Medal[] m_Medals_Other;
    public Text m_Label_Progress;
    public Image m_Image_Progress;

    public SpriteAtlas m_Atlas_UI;

    private void Awake()
    {
        Screen.SetResolution(720, 1280, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeOut();

        var gd = CM_Singleton<GameData>.instance;

        m_Image_Icon.sprite = gd.m_Atlas_UI.GetSprite(string.Format("img_etc_{0}", gd.m_Table_Stage.m_Dic[gd.m_StageId].m_StageImage));
        m_Label_Area.text = string.Format("지역{0}", gd.m_Table_Stage.m_Dic[gd.m_StageId].m_Id);
        m_Label_Name.text = gd.m_Table_Stage.m_Dic[gd.m_StageId].m_StageName;

        for (int i = 0; i < m_Medals_My.Length; i++)
        {
            if (i < gd.m_MyInfo.m_Dino.Count)
                m_Medals_My[i].Set(gd.m_MyInfo.m_Dino[i]);
            else
                m_Medals_My[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < m_Medals_Other.Length; i++)
        {
            if (i < gd.m_OtherInfo.m_Dino.Count)
                m_Medals_Other[i].Set(gd.m_OtherInfo.m_Dino[i]);
            else
                m_Medals_Other[i].gameObject.SetActive(false);
        }

        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        float Sec = 1.0f;
        float f = 0.0f;
        float r = 0.0f;

        while(true)
        {
            f += Time.deltaTime;
            r = Mathf.SmoothStep(0, 100, f / Sec);
            m_Label_Progress.text = string.Format("{0}%", r);
            m_Image_Progress.fillAmount = r/100;
            if (f >= Sec)
                break;

            yield return null;
        }

        GoBattle();
    }

    void GoBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    void FadeOut()
    {
        if (CM_Singleton<GameData>.instance.m_Util.m_Transitioner)
        {
            CM_Singleton<GameData>.instance.m_Util.m_Transitioner._transitionCamera = Camera.main;
            CM_Singleton<GameData>.instance.m_Util.m_Transitioner.TransitionInWithoutChangingScene();
        }
    }

    /*
    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 100, 100), "Tras"))
        {
            FadeOut();
        }
    }
    */
}
