using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler{

    private GameObject itemPrefab;

    public void Start()
    {
        itemPrefab = Resources.Load("Prefebs/Item/item") as GameObject;
    }

    public void StoreItem(Item item,Transform etransform)
    {
        if(etransform.childCount==0)
          Instantiate(itemPrefab, etransform).GetComponent<ItemUI>().SetItem(item);
        else
          etransform.GetChild(0).GetComponent<ItemUI>().AddAmount();
    }

    public void StoreItemWhenever(Item item, Transform transform,int amount)
    {
            Instantiate(itemPrefab, transform).GetComponent<ItemUI>().SetItem(item, amount);
    }
    /// <summary>
    /// 返回当前格子存的物品类型
    /// </summary>
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }

    public int GetItemID()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount == itemUI.Item.capacity;
    }

    public virtual void OnPointerExit(PointerEventData eventData)//离开格子区域
    {
        if (transform.childCount > 0)
        {
            UIManager.Instance.isToolTipShow = false;
            UIManager.Instance.HideToolTip();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)//进入格子区域
    {
        if (transform.childCount > 0)
        {
                UIManager.Instance.isToolTipShow = true;
                UIManager.Instance.ShowToolTip(transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText());
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)//开始拖拽
    {
        UIManager.Instance.preTrans = gameObject.transform;
        if (transform.childCount > 0)
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            UIManager.Instance.PickUpItem(currentItem.Item, currentItem.Amount);
            UIManager.Instance.slot = GetComponent<Slot>();
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public virtual void OnDrag(PointerEventData eventData)//拖拽中
    {
       
    }

    public virtual void OnEndDrag(PointerEventData eventData)//结束拖拽
    {
        if (UIManager.Instance.IsPickingItem == false)//没有选中的物体
        {
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject == null)//结束区域为空
        {
            Return();
            return;
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)//点击格子区域
    {
        if (transform.childCount > 0)
      {
            ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
            itemUI.Use();
            if (itemUI.Amount == 0)//用完了
            {
                DestroyImmediate(itemUI.gameObject);
                UIManager.Instance.isToolTipShow = false;
                UIManager.Instance.HideToolTip();
            }

        }
    }

    public void Exchange(ItemUI currentItem,Transform endTrans)//交换两个格子信息
    {
        Item item = currentItem.Item;
        int amount = currentItem.Amount;
        DestroyImmediate(endTrans.GetChild(0).gameObject);

        StoreItemWhenever(item, UIManager.Instance.preTrans, amount);
        StoreItemWhenever(UIManager.Instance.PickedItem.Item, endTrans, UIManager.Instance.PickedItem.Amount);

        UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
    }
    public void Return()//回退操作
    {
        StoreItemWhenever(UIManager.Instance.PickedItem.Item, UIManager.Instance.preTrans, UIManager.Instance.PickedItem.Amount);
        UIManager.Instance.RemoveItem(UIManager.Instance.PickedItem.Amount);
    }
}
