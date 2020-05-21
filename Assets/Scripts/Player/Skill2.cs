using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Skill2 : BaseSkill
{

    public override void Awake()
    {
        Lifetime = 0.6f;
        base.Awake();
        message = new object[2];
        message[0] = 1f; message[1] = 3;//倍数，次数
    }
}
