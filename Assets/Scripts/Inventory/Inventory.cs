using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Inventory : MonoBehaviour {
    
  
    
    protected  Slot[] slotList;

	public  virtual void Start () {
        slotList = GetComponentsInChildren<Slot>();
	}
	
    public bool GetItem(int id)//从外部通过id获取物品
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return GetItem(item);
    }
    public bool GetItem(Item item)//从外部通过引用获取物品
    {
        if(item==null)
        {
            Debug.LogError("id对应物品不存在");
            return false;
        }
        if(item.capacity==1)//不能叠加的物品，直接寻找空位
        {
           Slot slot= FindEmptySlot();
           if(slot==null)
            {
                //TODO 提示物品栏满了
                return false;
            }
           else
            {
                slot.StoreItem(item,slot.transform);//把物品传到空的物品槽
                return true;
            }
        }
        else //可以叠加的物品，分情况
        {
            Slot slot = FindSameIDSlot(item);//先看能不能叠加
            if(slot!=null)
            {
                slot.StoreItem(item, slot.transform);
                return true;
            }
            else
            {
                slot = FindEmptySlot();
                if (slot == null)
                {
                  //TODO
                    return false;
                }
                else
                {
                    slot.StoreItem(item, slot.transform);//把物品传到空的物品槽
                    return true;
                }
            }
        }
    }

    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if(slot.transform.childCount==0)
            return slot;
        }
        return null;
    }
    private Slot FindSameIDSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount >= 1&&slot.GetItemID()==item.ID && !slot.IsFilled())
            {
                return slot;
            }
        }
        return null;
    }
}
