using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Area : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPush()
    {
        GameObject.Find("Main").GetComponent<Lobby>().OnShow_Info(0);
    }
}
