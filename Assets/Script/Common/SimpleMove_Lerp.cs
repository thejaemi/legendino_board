using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove_Lerp : MonoBehaviour
{
    Vector3 m_StartPosition;
    public Vector3 m_EndPosition;

    private void Awake()
    {
        m_StartPosition = gameObject.transform.position;
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
        transform.position = StartPosition;
        m_StartPosition = StartPosition;
        m_EndPosition = EndPosition;
        OnStart(RunTime);
    }

    public void OnReset()
    {
        StopCoroutine("Run");
        gameObject.transform.position = m_StartPosition;
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
            gameObject.transform.position = Vector3.Lerp(m_StartPosition, m_EndPosition, (Time.time - StartTime) / RunTime);

            if (gameObject.transform.position == m_EndPosition)
                break;

            yield return null;
        }

        yield break;
    }
}
