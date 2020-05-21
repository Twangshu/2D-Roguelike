using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.Remoting.Lifetime;

public class Skill1 : BaseSkill
{
   
    public override void Awake()//继承Mono的最好别写构造函数，用生命周期函数替代
    {
        Lifetime = 0.5f;
        base.Awake();
        message = new object[2];
        message[0] = 1.5f;message[1] = 1;//倍数，次数
    }
}
