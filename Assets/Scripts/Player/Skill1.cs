using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.Remoting.Lifetime;

public class Skill1 : BaseSkill
{
    public Skill1()
    {
        Lifetime = 0.5f;
    }
    public override void Awake()
    {
        base.Awake();
        message = new object[2];
        message[0] = 1.5f;message[1] = 1;//倍数，次数
    }
}
