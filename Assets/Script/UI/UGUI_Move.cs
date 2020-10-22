using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUI_Move : MonoBehaviour
{
    Vector3 m_StartPosition;
    public Vector3 m_EndPosition;
    public bool m_AutoStart = false;
    public float m_RunTime = 1.0f;

    private void Awake()
    {        
        m_StartPosition = gameObject.GetComponent<RectTransform>().localPosition;
    }

    private void Start()
    {
        if(m_AutoStart)
            OnStart(m_RunTime);
    }

    public void OnStart(float RunTime)
    {
        OnReset();
        StartCoroutine(Run(RunTime));
    }

    public void OnStart(Vector3 EndPosition, float RunTime)
    {
        m_EndPosition = EndPosition;
        OnStart(RunTime);
    }

    public void OnStart(Vector3 StartPosition, Vector3 EndPosition, float RunTime)
    {
        gameObject.GetComponent<RectTransform>().localPosition = StartPosition;
        m_StartPosition = StartPosition;
        m_EndPosition = EndPosition;
        OnStart(RunTime);
    }

    public void OnReset()
    {
        StopCoroutine("Run");
        gameObject.GetComponent<RectTransform>().localPosition = m_StartPosition;
    }

    private void OnDestroy()
    {
        StopCoroutine("Run");
    }

    IEnumerator Run(float RunTime)
    {
        float StartTime = Time.time;

        while (true)
        {
            gameObject.GetComponent<RectTransform>().localPosition = Vector3.Lerp(m_StartPosition, m_EndPosition, (Time.time - StartTime) / RunTime);

            if (gameObject.GetComponent<RectTransform>().localPosition == m_EndPosition)
                break;

            yield return null;
        }

        yield break;
    }
}
