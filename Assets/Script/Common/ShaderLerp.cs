using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderLerp : MonoBehaviour
{
    public Material m_Material;
    public string m_Variable;

    public float m_Sec = 1.0f;
    float m_RunTime;

    public bool m_Active = false;

    public void Run()
    {
        StartCoroutine(Work());
    }

    private void Update()
    {
        if(m_Active == true)
        {
            m_Active = false;
            Run();
        }
    }

    IEnumerator Work()
    {
        m_RunTime = 0.0f;

        while(true)
        {
            m_RunTime += m_Sec * Time.deltaTime;
            m_Material.SetFloat(m_Variable, Mathf.Lerp(1.0f, 0.0f, m_RunTime));

            if (m_RunTime >= m_Sec)
                break;

            yield return null;
        }
    }
}
