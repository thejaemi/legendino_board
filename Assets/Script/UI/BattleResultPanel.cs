using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleResultPanel : MonoBehaviour
{
    public UILabel result;

    public void SetWinResultText()
    {
        result.text = "[0038FF]승리";
        gameObject.SetActive(true);
    }

    public void SetLoseResultText()
    {
        result.text = "[FF0000]패배";
        gameObject.SetActive(true);
    }

    public void SetDrawResultText()
    {
        result.text = "[BCBCBC]무승부";
        gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("Battle2");
    }

    public void OnClickMedalInputButton()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene("TestLobby");
#else
        SceneManager.LoadScene("Scan");
#endif
    }
}
