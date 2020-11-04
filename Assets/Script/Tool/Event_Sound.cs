using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Sound : MonoBehaviour
{
    public string m_SoundName = "";
    public bool m_Loop = false;

    private void OnEnable()
    {
        if (m_Loop)
            SoundManager.PlaySFXLoop(gameObject, m_SoundName);
        else
            SoundManager.PlaySFX(gameObject, m_SoundName);
    }
}
