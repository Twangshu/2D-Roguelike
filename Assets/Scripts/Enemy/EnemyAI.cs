using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {

    public Transform target;
    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    private  Path path;
    private  int currentWaypoint = 0;
    private  bool reachedEndOfPath = false;
    public int isInitDirRight;
    private  Seeker seeker;
    private  Rigidbody2D rb;
    private Vector3 initialScale;
    public bool isStop = false;
    public Vector3 test;

	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        initialScale = GetComponent<Enemy>().initialScale;
        test = initialScale;
        InvokeRepeating("SeekPath", 0, .5f);
        target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (path == null)
            return;
        if(currentWaypoint>=path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
       
        rb.velocity=direction * speed * Time.fixedDeltaTime;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        float dir = target.position.x - rb.position.x;
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        if (dir>= 0.01f)
        {
            transform.localScale = new Vector3(1 * isInitDirRight * initialScale.x, initialScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1 * isInitDirRight * initialScale.x, initialScale.y, 1);
        }
    }
    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void SeekPath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
}
