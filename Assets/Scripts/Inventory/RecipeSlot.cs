using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeSlot : Slot {

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
        if (endTrans.tag == "slot")//从配方栏往背包栏
        {

            if (endTrans.childCount > 0)//移动背包中有物品的地方
            {
                    Return();
                    return;
            }
            else//直接放进背包
            {
                StoreItem(UIManager.Instance.PickedItem.Item, endTrans);
                UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
               // ForgeManager.Instance.ClearRecipeInfo();
            }
        }
        else
        {
            Return();
        }
    }
}
