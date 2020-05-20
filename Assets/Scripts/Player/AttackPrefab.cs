using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackPrefab : MonoBehaviour {

    object[] message = new object[2];
    private void Awake()
    {
        Destroy(gameObject, 0.6f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
      
        if (collider.tag.CompareTo(Tags.Enemy)==0)
        {
            message[0] = 1f;//倍率
            message[1] = 1;//次数
            collider.SendMessage("takeDamage",message);
        }
        
    }
}
