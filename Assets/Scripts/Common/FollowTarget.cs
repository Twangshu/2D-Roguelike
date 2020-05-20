using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowTarget : MonoBehaviour {

    private GameObject player;
    private Vector2 targetPosion;
    public float speed = 0.2f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    private void Update()
    {
        //限定相机的边界范围
        // targetPosion = new Vector2( Mathf.Max( 6.63f,Mathf.Min(37, player.transform.position.x)-0.37f), Mathf.Max(3.48f,Mathf.Min(25.41f, player.transform.position.y- 1.52f)));
        targetPosion = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.DOMove(targetPosion,speed);
    }
}
