using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnableAfterDisable : MonoBehaviour
{
    /// <summary>
    /// 몇 초 후에 비활성화 할지..
    /// </summary>
    public float m_Delay;

    private void OnEnable()
    {
        Invoke("Run", m_Delay);
    }

    void Run()
    {
        gameObject.SetActive(false);
    }
}
