//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class MaterialSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//{

//    private GameObject item;
//    public Text amount;
//    private int needAmount=0;
//    private int nowAmount=0;
//    public void Start()
//    {
//        item = Resources.Load("Prefebs/Item/item") as GameObject;
//    }

//    public virtual void OnPointerExit(PointerEventData eventData)
//    {
//        if (transform.childCount > 0)
//        {
//            InventoryManager.Instance.isToolTipShow = false;
//            InventoryManager.Instance.HideToolTip();
//        }

//    }

//    public virtual void OnPointerEnter(PointerEventData eventData)
//    {
//        if (transform.childCount > 0)
//        {
//            InventoryManager.Instance.isToolTipShow = true;
//            InventoryManager.Instance.ShowToolTip(transform.GetChild(0).GetComponent<ItemUI>().item.GetToolTipText());

//        }

//    }
//    public void StoreItem(Item item, int amount)
//    {
//        if (gameObject.transform.childCount == 0)
//        {
//            this.amount.enabled = true;
//            GameObject itemgo = Instantiate(this.item, transform);
//            itemgo.transform.localPosition = Vector3.zero;
//            itemgo.transform.localScale = Vector3.one;
//            itemgo.GetComponent<ItemUI>().SetItem(item);

//            foreach(Slot slot in Knapsack.Instance.slotList)
//            {
//                if(slot.transform.childCount>0)
//                {
//                    ItemUI slotItem = slot.transform.GetChild(0).GetComponent<ItemUI>();
//                    if (slotItem.item==item)
//                    {
//                        nowAmount += slotItem.Amount;
//                    }
//                }
//            }
//            this.amount.text = nowAmount.ToString() + "/" + amount.ToString();
//        }
//        else
//        {
//            ClearSlot();
//            StoreItem(item, amount);
//        }

        
//    }
//    public void ClearSlot()
//    {
//        if (transform.childCount > 0)
//        {
//            DestroyImmediate(transform.GetChild(0).gameObject);
//            needAmount = 0;
//            nowAmount = 0;
//            amount.enabled = false;
//        }
           
//    }
//}
