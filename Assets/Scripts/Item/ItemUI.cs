using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemUI : MonoBehaviour {

    public Item Item { get; set; }
    public int Amount { get; set; }
    private Text itemAmount;
    private Image itemImage;
    public Text ItemAmount { get => itemAmount??GetComponentInChildren<Text>(); set => itemAmount = value; }
    public Image ItemImage { get => itemImage??GetComponent<Image>(); set => itemImage = value; }
    public Vector3 initScale = Vector3.one * 2;//用于控制大小变化效果
    public Vector3 targetScale = Vector3.one;
    [Header("变化时间")]
    public float speed = 0.2f;

    public void SetItem(Item item,int amount=1)
    {
        Item = item;
        Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.sprite);
        PlayAnim();
    }

    public void SetPickingItem(Item item, int amount = 1)//设置选中物体
    {
        Item = item;
        Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.sprite);
    }

    public void UpdateUI()
    {
        ItemAmount.text = Amount.ToString();
        ItemAmount.enabled = (Item is Equipment || Item is Recipe) ? false : true;
    }

    public void AddAmount(int amount=1)
    {
        Amount += amount;
        PlayAnim();
    }

    private void PlayAnim()//播放变大变小动画
    {
        transform.localScale = initScale;
        Tween tweener = transform.DOScale(targetScale, speed).SetUpdate(true);
        UpdateUI();
    }
    public void ReduceAmount(int amount = 1)
    {
        Amount -= amount;
        UpdateUI();
    }

    public void Show()//这两个可以优化掉
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
  
    public void SetItemUI(ItemUI itemUI)
    {
        SetItem(itemUI.Item, itemUI.Amount);
    }

    public void Use()
    {
        if(Item is Consumable)
        {
            PlayInfoManager.Instance.currentHP += ((Consumable)Item).HP;
            PlayInfoManager.Instance.currentMP += ((Consumable)Item).MP;
            PlayInfoManager.Instance.UpdateState();
            ReduceAmount();
        }
    }
}
