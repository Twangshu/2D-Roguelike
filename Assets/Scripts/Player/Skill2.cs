using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Skill2 : BaseSkill
{


    public Skill2()
    {
        Lifetime = 0.6f;
    }
    public override void Awake()
    {
        base.Awake();
        message = new object[2];
        message[0] = 1f; message[1] = 3;//倍数，次数
    }
}
