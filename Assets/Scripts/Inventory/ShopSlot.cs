using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : Slot {

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        Transform endTrans = eventData.pointerCurrentRaycast.gameObject.transform;

        Item buyItem = UIManager.Instance.PickedItem.Item;//获取选择物品信息
        int amount = UIManager.Instance.PickedItem.Amount;

        if (endTrans.tag == Tags.KnapsackSlot)//从商店栏往背包栏（买东西）
        {
            if(PlayInfoManager.Instance.gold<buyItem.buyPrice*amount)//钱不够
            {
                Return();
                PlayInfoManager.Instance.NotEnoughMoney();
                return;
            }
            if (endTrans.childCount > 0)//移动背包中有物品的地方
            {
                ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();//背包里的物品
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
            else//放进对应格子
            {
                for (int i = 0; i < UIManager.Instance.PickedItem.Amount; i++)
                {
                    StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                }
                UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
            }
            PlayInfoManager.Instance.gold -= buyItem.buyPrice * amount;//购买成功，扣钱
            PlayInfoManager.Instance.UpdateGoldAmount();
        }
        else//先不允许从商店直接移到商店或装备栏
        {
            Return();
            return;
        }
    }
}
