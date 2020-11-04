using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_Battle()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Button_Starter()
    {
        SoundManager.PlaySFX(gameObject, "fx_u_0034");
        Application.OpenURL("https://smartstore.naver.com/thejaemi/products/2482391702");
    }

    public void Button_Booster()
    {
        SoundManager.PlaySFX(gameObject, "fx_u_0034");
        Application.OpenURL("https://smartstore.naver.com/thejaemi/products/2701018112");
    }
}
