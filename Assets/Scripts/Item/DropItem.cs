using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

    public Item item { get; set; }
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }
    public void SetItem(Item item)
    {
        this.item = item;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(item.sprite);
        transform.localScale = new Vector3(.25f, .25f, .25f);
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space)&& (player.transform.position - transform.position).magnitude < 1f)
        {
            Knapsack.Instance.GetItem(item);
            Destroy(gameObject);
        }
    }

}
