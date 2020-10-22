using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    public float m_DelaySec = 1.0f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_DelaySec);

        gameObject.GetComponent<Animation>().Play();
    }
}
