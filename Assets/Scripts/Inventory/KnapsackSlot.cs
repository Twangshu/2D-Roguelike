using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnapsackSlot : Slot
{

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Transform endTrans = eventData.pointerCurrentRaycast.gameObject.transform;

        if (endTrans.tag == Tags.KnapsackSlot)
        {
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
                            else
                            {
                                int PreAmount = currentItem.Amount;
                                currentItem.Amount = currentItem.Item.capacity;//先填满
                                currentItem.UpdateUI();
                                for (int i = 0; i < UIManager.Instance.PickedItem.Amount - (currentItem.Item.capacity - PreAmount); i++)//其余的补到其他格子
                                {
                                    Knapsack.Instance.GetItem(UIManager.Instance.PickedItem.Item);
                                }
                                UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                            }

                        }
                        else//无容量
                        {
                            Exchange(currentItem, endTrans);

                        }
                    }
                    else//ID不同，交换
                    {
                        Exchange(currentItem, endTrans);

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



        }
        else if (endTrans.tag == Tags.EquipmentSlot)
        {

            if (UIManager.Instance.PickedItem.Item is Equipment && ((Equipment)UIManager.Instance.PickedItem.Item).equipmentType == endTrans.gameObject.GetComponent<EquipmentSlot>().equipmentType)//类型匹配
            {
                if (endTrans.childCount > 0)//这个格子已经有东西了
                {
                    ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();
                    Exchange(currentItem, endTrans);
                    PlayInfoManager.Instance.UpdatePlayInfo();
                }
                else//没东西
                {
                    StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                    UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                    PlayInfoManager.Instance.UpdatePlayInfo();
                }
            }
            else//不是同种装备或不是装备，直接返回
            {
                Return();
                return;
            }
        }
        else if (endTrans.tag == Tags.ShopSlot)//卖出物品
        {
            Item soldItem = UIManager.Instance.PickedItem.Item;
            int amount = UIManager.Instance.PickedItem.Amount;
            if (endTrans.childCount > 0)//商店格子已经有东西了
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
                        else//找新的格子继续填
                        {
                            if (!Shop.Instance.GetItem(UIManager.Instance.PickedItem.Item))//商店也没格子了
                            {
                                Return();
                                return;
                            }
                            int PreAmount = currentItem.Amount;
                            currentItem.Amount = currentItem.Item.capacity;//先填满
                            currentItem.UpdateUI();
                            for (int i = 0; i < UIManager.Instance.PickedItem.Amount - (currentItem.Item.capacity - PreAmount) - 1; i++)//其余的补到其他格子
                            {
                                Shop.Instance.GetItem(UIManager.Instance.PickedItem.Item);
                            }
                            UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                        }


                    }
                    else//容量满了
                    {
                        if (!Shop.Instance.GetItem(UIManager.Instance.PickedItem.Item))//商店也没格子了
                        {
                            Return();
                            return;
                        }
                        for (int i = 0; i < UIManager.Instance.PickedItem.Amount - 1; i++)//全部移到新的格子
                        {
                            Shop.Instance.GetItem(UIManager.Instance.PickedItem.Item);
                        }
                        UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                    }
                }
                else//ID不同，自动寻找格子填上去
                {
                    if (!Shop.Instance.GetItem(UIManager.Instance.PickedItem.Item))//商店也没格子了
                    {
                        Return();
                        return;
                    }
                    for (int i = 0; i < UIManager.Instance.PickedItem.Amount - 1; i++)//全部加到商店格子
                    {
                        Shop.Instance.GetItem(UIManager.Instance.PickedItem.Item);
                    }
                    UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                }
            }
            else//没东西
            {
                for (int i = 0; i < UIManager.Instance.PickedItem.Amount; i++)//全部加到商店格子
                {
                    StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                }
                UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
            }
            PlayInfoManager.Instance.gold += (soldItem.sellPrice * amount);
            PlayInfoManager.Instance.UpdateGoldAmount();
        }
        else if (endTrans.tag.CompareTo(Tags.ShortCutSlot) == 0)//从背包栏往快捷栏
        {
            if (!(UIManager.Instance.PickedItem.Item is Consumable))
            {
                Return();
                return;
            }
            else
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
        }
        //else if (endTrans.tag == "RecipeSlot")//从背包栏往配方栏
        //{
        //    Item pickedItem = UIManager.Instance.PickedItem.item;
        //    if (pickedItem is Recipe)
        //    {
        //        if (endTrans.childCount > 0)//移动背包中有物品的地方
        //        {
        //            ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();//背包里的物品
        //            Exchange(currentItem, endTrans);
        //            ForgeManager.Instance.UpdateRecipeInfo(pickedItem as Recipe);
        //        }
        //        else//直接放进配方栏
        //        {
        //            Recipe recipe = pickedItem as Recipe;
        //            ForgeManager.Instance.UpdateRecipeInfo(recipe);
        //            StoreItem(UIManager.Instance.PickedItem.item, endTrans);

        //            UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
        //        }
        //    }
        //    else
        //    {
        //        Return();
        //        return;
        //    }

        //}
        else
        {
            Return();
            return;
        }
    }

   
}
