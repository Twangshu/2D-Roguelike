using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 消耗品类
/// </summary>
public class Consumable :Item {

	public int HP { get; set; }
    public int MP { get; set; }

    public Consumable(int id, string name, ItemType itemType, Quality quality, string des, int capacity, int buyPrice, int sellPrice,int hp,int mp,string sprite) : base(id, name, itemType, quality, des,capacity, buyPrice, sellPrice,sprite)
    {
        HP = hp;
        MP = mp;
    }

    public override string ToString()
    {
        //StringBuilder s = new StringBuilder();
        //s.Append(ID.ToString());
        string s = "";
        s += ID.ToString();
        s += Type;
        s += quality;
        s += description;
        s += buyPrice;
        s += sellPrice;
        s += sprite;
        s += HP;
        s += MP;
        return s;
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string newText = string.Format("{0}\n\n<color=blue>加血：{1}\n加蓝：{2}</color>", text, HP, MP);

        return newText;
}
}
