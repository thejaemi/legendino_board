using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShake : MonoBehaviour
{
    Camera m_Camera;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        var elapsed = 0f;

        while (elapsed < duration)
        {
            var val = Mathf.Min(Random.Range(magnitude, 1.0f), 1.0f);

            m_Camera.orthographicSize = val;

            elapsed += Time.deltaTime;

            yield return null;
        }

        //Debug.LogFormat("CameraSize : {0}", m_Camera.orthographicSize);

        while (m_Camera.orthographicSize < 0.9f)
        {
            m_Camera.orthographicSize =  Mathf.Lerp(m_Camera.orthographicSize, 1.0f, 0.1f);
            //Debug.LogFormat("CameraSize : {0}", m_Camera.orthographicSize);

            yield return null;
        }

        m_Camera.orthographicSize = 1.0f;
    }

    /*
    private void OnGUI()
    {
        if(GUI.Button(new Rect(100, 100, 100, 100), "Shake"))
        {
            Shake(0.2f, 0.7f);
        }
    }
    */
}
