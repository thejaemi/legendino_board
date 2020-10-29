using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUI_Scale : MonoBehaviour
{
    Vector3 m_StartScale;
    public Vector3 m_EndScale;
    public bool m_AutoStart = false;
    public float m_RunTime = 1.0f;

    private void Awake()
    {
        m_StartScale = gameObject.GetComponent<RectTransform>().localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_AutoStart)
            OnStart(m_RunTime);
    }

    public void OnStart(float RunTime)
    {
        OnReset();
        StartCoroutine(Run(RunTime));
    }

    public void OnStart(Vector3 EndPosition, float RunTime)
    {
        m_EndScale = EndPosition;
        OnStart(RunTime);
    }

    public void OnStart(Vector3 Start, Vector3 End, float RunTime)
    {
        gameObject.GetComponent<RectTransform>().localScale = Start;
        m_StartScale = Start;
        m_EndScale = End;
        OnStart(RunTime);
    }

    public void OnReset()
    {
        StopCoroutine("Run");
        gameObject.GetComponent<RectTransform>().localScale = m_StartScale;
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
            gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(m_StartScale, m_EndScale, (Time.time - StartTime) / RunTime);

            if (gameObject.GetComponent<RectTransform>().localScale == m_EndScale)
                break;

            yield return null;
        }

        yield break;
    }
}
