using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(50, 50, 100, 100), "Test"))
        {
            if (CM_Singleton<GameData>.instance.m_Util.m_Transitioner)
            {
                CM_Singleton<GameData>.instance.m_Util.m_Transitioner._transitionCamera = Camera.main;
                CM_Singleton<GameData>.instance.m_Util.m_Transitioner.TransitionInWithoutChangingScene();
            }
        }
    }
}
