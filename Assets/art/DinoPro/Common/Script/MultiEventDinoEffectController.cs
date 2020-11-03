using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEventDinoEffectController : DinoEffectController
{
    //이펙트를 몇개 만들건지 설정 - 기본값은 1개
    [Range(1, 100)]
    public int count = 1;


}
