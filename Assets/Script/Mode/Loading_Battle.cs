using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Battle : MonoBehaviour
{
    public UILabel m_Label_Title;
    public Medal[] m_Medals_My;
    public Medal[] m_Medals_Other;
    public UILabel m_Label_Progress;

    // Start is called before the first frame update
    void Start()
    {
        var gd = CM_Singleton<GameData>.instance;

        m_Label_Title.text = gd.m_Table_Stage.m_Dic[gd.m_StageId].m_StageName;

        for (int i = 0; i < m_Medals_My.Length; i++)
        {
            if (i < gd.m_MyDino.Count)
                m_Medals_My[i].Set(gd.m_MyDino[i]);
            else
                m_Medals_My[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < m_Medals_Other.Length; i++)
        {
            if (i < gd.m_OtherDino.Count)
                m_Medals_Other[i].Set(gd.m_OtherDino[i]);
            else
                m_Medals_Other[i].gameObject.SetActive(false);
        }

        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        float Sec = 1.0f;
        float f = 0.0f;

        while(true)
        {
            f += Time.deltaTime;
            m_Label_Progress.text = string.Format("{0}%", Mathf.SmoothStep(0, 100, f/Sec));
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
}
