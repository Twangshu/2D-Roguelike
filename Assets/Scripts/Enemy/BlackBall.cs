using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBall : MonoBehaviour {

    private GameObject player;
    Vector2 dir;
    public float speed = 6f;
    private Rigidbody2D rigidbody;
    // Use this for initialization
    void Start () {
        FindPlayer();
        dir = (player.transform.position - transform.position).normalized;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = dir*speed;
	}

    // Update is called once per frame
    void FindPlayer()
    {

        if (player == null)
            player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag.CompareTo(Tags.Player)==0)
        {
            FindPlayer();
            player.GetComponent<PlayerAct>().takeDamage(20,"黑龙骑士");
            Destroy(gameObject);
        }
        if(collider.tag.CompareTo(Tags.outWall) == 0)
        {
            Destroy(gameObject);
        }
    }
}
