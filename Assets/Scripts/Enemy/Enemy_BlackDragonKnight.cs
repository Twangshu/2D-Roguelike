using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlackDragonKnight : Enemy
{
    private  GameObject blackBall;

    private void Start()
    {
        blackBall = Resources.Load("Prefebs/Monster/Effect/BlackBall") as GameObject;
    }
    public override void AttackEffect()
    {
        Vector3 offset = transform.localScale == initialScale ? Vector3.left : Vector3.right;
        Instantiate(blackBall, transform.position +offset, Quaternion.identity);
    }
}
