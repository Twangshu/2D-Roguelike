using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class DropManager:MonoBehaviour
{
    Dictionary<Item, List<int>> itemAndDrop = new Dictionary<Item, List<int>>();
    private GameObject item;


    private void Start()
    {
        item = Resources.Load("Prefebs/Item/item") as GameObject;
    }
    public void DropAItem(int id,Vector3 dropPositon)
    {
        Item dropItem = InventoryManager.Instance.GetItemById(id);
        GameObject itemgo = Instantiate(item, dropPositon,Quaternion.identity);
        itemgo.GetComponent<ItemUI>().SetItem(dropItem);
    }
}