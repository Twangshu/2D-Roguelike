using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot {

    public Equipment.EquipmentType equipmentType;

    public int ATK = 0;
    public int DEF = 0;
    public int HP = 0;
    public int MP = 0;


    
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


        if (endTrans.tag == "slot")//从装备栏往背包栏
        {

            if (endTrans.childCount > 0)//移动背包中有物品的地方
            {
                ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();//背包里的物品
                if (currentItem.Item is Equipment && ((Equipment)currentItem.Item).equipmentType == this.equipmentType)//背包里物品也是装备且与选中物品类型相同
                {
                    ItemUI pickedItem = UIManager.Instance.PickedItem;
                    Exchange(currentItem, endTrans);
                    PlayInfoManager.Instance.UpdatePlayInfo();
                }
                else//不可取，送回去
                {
                    Return();
                    return;
                }
            }
            else//直接放进背包
            {
                StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                PlayInfoManager.Instance.UpdatePlayInfo();
            }
        }
        else if (endTrans.tag == "equipmentSlot")//从装备栏往装备栏
        {
            if (endTrans.childCount > 0)//移动装备栏中有物品的地方
            {
                ItemUI currentItem = endTrans.GetChild(0).GetComponent<ItemUI>();//装备栏里的物品
                if (((Equipment)currentItem.Item).equipmentType == this.equipmentType)//也只有都是首饰才会出现的情况了
                {
                    ItemUI pickedItem = UIManager.Instance.PickedItem;
                    Exchange(currentItem, endTrans);
                    PlayInfoManager.Instance.UpdatePlayInfo();
                }
                else//不可取，送回去
                {
                    Return();
                    return;
                }
            }
            else //移动到装备栏的空格
            {
                Equipment.EquipmentType TargetType = endTrans.gameObject.GetComponent<EquipmentSlot>().equipmentType;
                if (TargetType==equipmentType)//把首饰换个位置
                {
                    StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                    UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
                    PlayInfoManager.Instance.UpdatePlayInfo();
                }
                else//别乱移
                {
                    Return();
                    return;
                }
                
            }
        }

        else//不能直接把穿上的装备卖掉
        {
            Return();
            return;
        }
        }
    
    private void UpdateEquipmentInfo()
    {
        if(transform.childCount>0)
        {
            Equipment currentEquipment = (Equipment) transform.GetChild(0).gameObject.GetComponent<Item>();
            ATK = currentEquipment.ATK;
            DEF = currentEquipment.DEF;
            HP = currentEquipment.HP;
            MP = currentEquipment.MP;
        }
    }
}
