using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSkillEffectController : DinoEffectController
{
    public int skillTypeIndex;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
