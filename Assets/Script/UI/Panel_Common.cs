using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel_Common : MonoBehaviour
{
    public GameObject m_Dlg_Quit;
    public GameObject m_Dlg_BattleQuit;

    private void Awake()
    {

    }

    public void OnBack()
    {
        if (SceneManager.GetActiveScene().name == "Lobby" || SceneManager.GetActiveScene().name == "Shop" ||
            SceneManager.GetActiveScene().name == "Scan")
            m_Dlg_Quit.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Battle")
            m_Dlg_BattleQuit.SetActive(true);
    }

    public void OnBattle_Quit()
    {

    }

    public void OnBattle_Cancel()
    {

    }

    public void OnQuit_OK()
    {
        Application.Quit();
    }

    public void OnQuit_Cancel()
    {
        m_Dlg_Quit.SetActive(false);
    }
}
