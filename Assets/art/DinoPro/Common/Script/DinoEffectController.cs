using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DinoEffectDepthType
{
    above,
    below,
}

[ExecuteInEditMode]
public class DinoEffectController : EffectController
{
    public string animationName = "none";
    public string eventName = "none";
    public DinoEffectDepthType effectDepthType;

    public void PlayDinoEffect()
    {
        gameObject.SetActive(false); 
        gameObject.SetActive(true); 
    }

    public void SetDepth(int depth)
    {
        if(effectDepthType == DinoEffectDepthType.above)
            SetEffectAbove(depth);
        else
            SetEffectBelow(depth);
    }
}
