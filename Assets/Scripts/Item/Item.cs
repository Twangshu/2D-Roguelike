using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item  {


    public int ID { get; set; }
    public string name { get; set; }
    public ItemType Type { get; set; }
    public Quality quality { get; set; }
    public string description { get; set; }
    public int capacity { get; set; }
    public int buyPrice { get; set; }
    public int sellPrice { get; set; }
    public string sprite { get; set; }

    public Item()
    {
        this.ID = -1;
    }

    public Item(int id,string name,ItemType itemType,Quality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite)
    {
        this.ID = id;
        this.name = name;
        this.Type = itemType;
        this.quality = quality;
        this.description = des;
        this.capacity = capacity;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
        this.sprite = sprite;
    }
    /// <summary>
    /// 物品类别
    /// </summary>
    public  enum ItemType
    {
        Consumable,
        Equipment,
        Material,
        Recipe,
    }
    /// <summary>
    /// 品质
    /// </summary>
    public  enum Quality
    {
        Common,
        Rare,
        Epic,
        Legendary,
    }

    public virtual string GetToolTipText()
    {
        string color = "";
        switch (quality)
        {
            case Quality.Common:
                color = "white";
                break;
            case Quality.Rare:
                color = "navy";
                break;
            case Quality.Epic:
                color = "magenta";
                break;
            case Quality.Legendary:
                color = "orange";
                break;
        }
        string text = string.Format("<color={4}>{0}</color>\n<size=20><color=green>购买价格：{1} 出售价格：{2}</color></size>\n<color=yellow><size=20>{3}</size></color>", name, buyPrice, sellPrice, description, color);
        return text;
    }
}
