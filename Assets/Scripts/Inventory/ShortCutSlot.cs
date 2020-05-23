using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortCutSlot : Slot {

    public KeyCode keycode;//快捷按键

    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if (transform.childCount > 0)
            {
                ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
                itemUI.Use();
                if (itemUI.Amount == 0)
                {
                    DestroyImmediate(itemUI.gameObject);
                    UIManager.Instance.isToolTipShow = false;
                    UIManager.Instance.HideToolTip();
                }
            }
        }
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (UIManager.Instance.IsPickingItem == false)//选中包里的物体
        {
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {

            Return();
            return;
        }
        Transform endTrans = eventData.pointerCurrentRaycast.gameObject.transform;
        if(endTrans.tag.CompareTo(Tags.ShortcutSlot)==0)//从快捷栏拖到快捷栏
        {
            if (endTrans.childCount > 0)//移动到快捷栏中有物品的地方
            {
                ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();//快捷栏里的物品
                ItemUI pickedItem = UIManager.Instance.PickedItem;
                Exchange(currentItem, endTrans);
            }
            else //移动到快捷栏的空格
            {

                for (int i = 0; i < UIManager.Instance.PickedItem.Amount; i++)//全部加到商店格子
                {
                    StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                }
                UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
            }
        }
        else if(endTrans.tag.CompareTo(Tags.KnapsackSlot) == 0)
        {
                    if (endTrans.childCount > 0)//这个格子已经有东西了
                    {
                        ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();
                        if (currentItem.Item.ID == UIManager.Instance.PickedItem.Item.ID)//放在同一ID的物体上，叠加
                        {
                            if (currentItem.Item.capacity > currentItem.Amount)//还有容量
                            {
                                int amountRemain = currentItem.Item.capacity - currentItem.Amount;
                                if (amountRemain >= UIManager.Instance.PickedItem.Amount)
                                {
                                    currentItem.AddAmount(UIManager.Instance.PickedItem.Amount);
                                    UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                                }
                                else//
                                {

                                    if (!Knapsack.Instance.GetItem(UIManager.Instance.PickedItem.Item))
                                    {
                                        Return();
                                        return;

                                    }
                                    else
                                    {
                                        for (int i = 0; i < UIManager.Instance.PickedItem.Amount - 1; i++)//其余的补到其他格子
                                        {
                                            Knapsack.Instance.GetItem(UIManager.Instance.PickedItem.Item);
                                        }
                                        UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                                    }
                                }

                            }
                            else//无容量
                            {
                                if (!Knapsack.Instance.GetItem(UIManager.Instance.PickedItem.Item))
                                {
                                    Return();
                                    return;

                                }
                                else
                                {
                                    for (int i = 0; i < UIManager.Instance.PickedItem.Amount - 1; i++)//其余的补到其他格子
                                    {
                                        Knapsack.Instance.GetItem(UIManager.Instance.PickedItem.Item);
                                    }
                                    UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                                }
                            }
                        }
                    }
                    else//这个格子上没东西
                    {
                        for (int i = 0; i < UIManager.Instance.PickedItem.Amount; i++)
                        {
                            StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                        }
                        UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                    }

        }
            else
        {
            Return();
            return;
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
   
    }

}
