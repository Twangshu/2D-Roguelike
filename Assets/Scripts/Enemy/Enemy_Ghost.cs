﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    public override void AttackEffect()
    {
        base.AttackEffect();
        animator.SetTrigger("Attack");
    }
}
