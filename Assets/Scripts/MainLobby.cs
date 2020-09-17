using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    public GameObject buttonOfMedalInput;
    public GameObject buttonOfBattleScene;
    public GameObject[] medals;

    void Start()
    {
        CheckMedals();
    }

    private void CheckMedals()
    {
        for(int i = 0; i < medals.Length; i++)
        {
            if(medals[i].activeInHierarchy == false)
            {
                buttonOfMedalInput.SetActive(true);
                return;
            }
        }
        buttonOfMedalInput.SetActive(false);
        buttonOfBattleScene.SetActive(true);
    }

    public void OnClickMedalQRCodeInput()
    {
        for(int i = 0; i < medals.Length; i++)
        {
            if(medals[i].activeInHierarchy == false)
            {
                medals[i].SetActive(true);
                break;
            }
        }

        CheckMedals();
    }
    
    public void OnClickBattleButton()
    {
        SceneManager.LoadScene("BattleScene2");
    }
}
