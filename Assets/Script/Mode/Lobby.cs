using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject m_Panel_Select;
    public GameObject m_Panel_Info;

    public int m_CurTab = 0;
    //public GameObject m_Tab_Battle;
    //public GameObject m_Tab_Shop;


    public GameObject m_Scroll_Item;
    public GameObject m_Scroll_Grid;

    public bool m_Debug = false;

    // Start is called before the first frame update
    void Start()
    {
        if (CM_Singleton<GameData>.instance.m_UseFadeOut)
        {
            CM_Singleton<GameData>.instance.m_UseFadeOut = false;
            CM_Singleton<GameData>.instance.m_Util.FadeOut();
        }

        for (int i=CM_Singleton<GameData>.instance.m_Table_Stage.m_Dic.Count-1; i>=0; i--)
            (Instantiate(m_Scroll_Item, m_Scroll_Grid.transform) as GameObject).GetComponent<Button_Area>().Init(i+1);

        m_Scroll_Grid.GetComponent<UIGrid>().enabled = true;

        //m_Tab_Battle.SetActive(true);
        //m_Tab_Shop.SetActive(false);

        CM_Singleton<GameData>.instance.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OnShow_Info(int n)
    {
        m_Panel_Info.GetComponent<Panel_StageInfo>().Set(n);
        m_Panel_Info.SetActive(true);
    }

    private void OnGUI()
    {
        if (!m_Debug)
            return;

        if (GUI.Button(new Rect(50, 50, 100, 100), "Reset"))
            CM_Singleton<GameData>.instance.ResetStageStep();
    }
}
