using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : Item {

    public int ID1 { get; set; }
    public int amount1 { get; set; }
    public int ID2 { get; set; }
    public int amount2 { get; set; }
    public int ID3 { get; set; }
    public int amount3 { get; set; }
    public int spawnID { get; set; }

    public Recipe(int id, string name, ItemType itemType, Quality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite,int id1,int id2,int id3,int amount1,int amount2,int amount3,int spawnID) : base(id, name, itemType, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        ID1 = id1;
        ID2 = id2;
        ID3 = id3;
        this.amount1 = amount1;
        this.amount2 = amount2;
        this.amount3 = amount3;
        this.spawnID = spawnID;
    }
}
