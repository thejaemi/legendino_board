using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDinoEffectController : DinoEffectController
{
    public enum Bone
    {
        EffBorn_Body,
        EffBorn_Root,
    }

    public Bone selectedBone;
    public bool followTargetBone = false;

    private Transform m_originParent;
    private Vector3 m_originLocalPosition;
    private Vector3 m_originLocalScale;
    // private Vector3 m_originLocalEulerAngles;

    public void SetOriginParent()
    {
        // Debug.Log(transform.parent.name);
        m_originParent = transform.parent;
        m_originLocalPosition = Vector3.zero;
        // m_originLocalPosition = transform.localPosition;
        // m_originLocalScale = transform.localScale;
        // m_originLocalEulerAngles = transform.localEulerAngles;
    }

    public void SetFlip()
    {
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
    }

    

    public void PlayTargetEffect(Transform target)
    {
        string boneName = selectedBone.ToString();
        Transform targetTransform = target.Find(boneName);
        if(targetTransform == null)
            return;

        if(followTargetBone)
        {
            transform.SetParent(targetTransform);
            transform.localPosition = m_originLocalPosition;
        }
        else
        {
            transform.SetParent(target);
            transform.localPosition = targetTransform.localPosition;   
        }
        

        PlayDinoEffect();
    }

    public void PlayTargetEffect(MonoBehaviour behaviour, Transform target)
    {
        string boneName = selectedBone.ToString();
        Transform targetTransform = target.Find(boneName);
        if(targetTransform == null)
            return;

        if(followTargetBone)
        {
            transform.SetParent(targetTransform);
            transform.localPosition = m_originLocalPosition;
        }
        else
            transform.SetParent(target);

        behaviour.StartCoroutine(DelayPlay(targetTransform));
    }

    private IEnumerator DelayPlay(Transform target)
    {
        // 8f/30f = 267
        yield return YieldHelper.waitForSeconds(267);
        if(followTargetBone == false)
            transform.localPosition = target.localPosition;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    protected void OnDead()
    {
        transform.SetParent(m_originParent);
        transform.localPosition = m_originLocalPosition;
        // transform.localScale = m_originLocalScale;
        // transform.localEulerAngles = m_originLocalEulerAngles;
    }
}
