using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation_Stop : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        gameObject.GetComponent<Animation>().Stop();
    }

    public void Play()
    {
        gameObject.GetComponent<Animation>().Play();
    }
}
