using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject m_Panel_Select;
    public GameObject m_Panel_Info;

    public GameObject m_Scroll_Item;
    public GameObject m_Scroll_Grid;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(m_Scroll_Item, m_Scroll_Grid.transform);
        Instantiate(m_Scroll_Item, m_Scroll_Grid.transform);
        Instantiate(m_Scroll_Item, m_Scroll_Grid.transform);
        Instantiate(m_Scroll_Item, m_Scroll_Grid.transform);
        Instantiate(m_Scroll_Item, m_Scroll_Grid.transform);

        m_Scroll_Grid.GetComponent<UIGrid>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShop()
    {
        SceneManager.LoadScene("Shop");
    }


    public void OnBattle()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene("TestLobby");
#else
        SceneManager.LoadScene("Scan");
#endif
    }

    public void OnShow_Info(int n)
    {
        m_Panel_Info.SetActive(true);
    }

    public void OnClose_Info()
    {
        m_Panel_Info.SetActive(false);
    }
}
