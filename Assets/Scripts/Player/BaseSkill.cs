using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{

    protected float Lifetime=0;
    protected object[] message;

    
    public virtual void Awake()
    {
        Destroy(gameObject,Lifetime);
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag.CompareTo(Tags.Enemy) == 0)
        {
            collider.SendMessage("takeDamage", message);
        }

    }
}
