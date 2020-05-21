using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    private static Shop _instance;

    public static Shop Instance
    {
        get
        {
            return _instance;
        }

    }
    protected ShopSlot[] shopSlotList;
    public Button close_Button;

    public void Start()
    {
        _instance = this;
        shopSlotList = GetComponentsInChildren<ShopSlot>();
        EventCenter.AddListener(EventDefine.ShowShopPanel, ShowThis);
        EventCenter.AddListener(EventDefine.CloseShopPanel, CloseThis);
        close_Button.onClick.AddListener(CloseThis);
        foreach (ShopSlot slot in shopSlotList)
        {
            slot.Start();
        }
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel, ShowThis);
        EventCenter.RemoveListener(EventDefine.CloseShopPanel, CloseThis);
    }

    private void ShowThis()
    {
        gameObject.SetActive(true);
    }
    public bool GetItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return GetItem(item);
    }
    public bool GetItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("id对应物品不存在");
            return false;
        }
        if (item.capacity == 1)
        {
            ShopSlot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.Log("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item, slot.transform);//把物品传到空的物品槽
                return true;
            }
        }
        else
        {
            ShopSlot slot = FindSameIDSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item, slot.transform);
                return true;
            }
            else
            {
                slot = FindEmptySlot();
                if (slot == null)
                {
                    Debug.Log("没有空的物品槽");
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

    private ShopSlot FindEmptySlot()
    {
        foreach (ShopSlot slot in shopSlotList)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
    }
    private ShopSlot FindSameIDSlot(Item item)
    {
        foreach (ShopSlot slot in shopSlotList)
        {
            if (slot.transform.childCount >= 1 && slot.IsFilled() == false && slot.GetItemID() == item.ID)
            {
                return slot;
            }
        }
        return null;
    }
    void CloseThis()
    {
        gameObject.SetActive(false);
    }
}