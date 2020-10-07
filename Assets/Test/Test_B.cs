using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_B : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CM_Dispatcher.IsDestroyed)
            if (CM_Dispatcher.instance)
                Debug.Log("Make");

        CM_JobQueue.Make()
            .Enqueue(Job())
            .Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Job()
    {
        Debug.Log("Job Start");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Job End");
    }
}
