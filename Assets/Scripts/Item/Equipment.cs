using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备类
/// </summary>
public class Equipment : Item {
    public int ATK { get; set; }
    public int DEF { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int crit { get; set; }
    public int dodge { get; set; }
    public EquipmentType equipmentType { get; set; }
    

    public Equipment(int id, string name, ItemType itemType, Quality quality, string des, int capacity, int buyPrice, int sellPrice, int atk, int def,int hp,int mp,EquipmentType equipmentType,string sprite) : base(id, name, itemType, quality, des,capacity, buyPrice, sellPrice,sprite)
    {
        ATK = atk;
        DEF = def;
        HP = hp;
        MP = mp;
        this.equipmentType = equipmentType;
    }

    public enum EquipmentType
    {
        Weapon,
        Head,
        Armor,
        Bracer,
        Boots,
        Document,
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string equipTypeText = "";
        switch (equipmentType)
        {
            case EquipmentType.Head:
                equipTypeText = "头部";
                break;
            case EquipmentType.Armor:
                equipTypeText = "护甲";
                break;
            case EquipmentType.Bracer:
                equipTypeText = "护腕";
                break;
            case EquipmentType.Boots:
                equipTypeText = "靴子";
                break;
            case EquipmentType.Weapon:
                equipTypeText = "武器";
                break;
            case EquipmentType.Document:
                equipTypeText = "饰品";
                break;
        }

        string newText = string.Format("{0}\n\n<color=blue>装备类型：{1}\nATK+{2}\nDEF+{3}\nHP+{4}\nMP+{5}</color>", text, equipTypeText, ATK, DEF, HP, MP);

        return newText;
    }
}
