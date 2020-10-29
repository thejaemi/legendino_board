using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mng_Util : MonoBehaviour
{
    public Transitioner m_Transitioner;

    private void Awake()
    {
        m_Transitioner = (Instantiate(Resources.Load("UGUI/WiggleDiamondTransitioner"), gameObject.transform) as GameObject).GetComponent<Transitioner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        if (m_Transitioner)
        {
            m_Transitioner._transitionCamera = Camera.main;
            m_Transitioner.TransitionInWithoutChangingScene();
        }
    }
}
