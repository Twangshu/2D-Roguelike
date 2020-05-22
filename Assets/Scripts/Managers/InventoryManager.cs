using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

   
    private static InventoryManager _instance;
    public static InventoryManager Instance { get => _instance; }

    private List<Item> itemList = new List<Item>();//初始化list（数组不能这样初始化）
    

    private void Start()
    {
        _instance = this;
        ParseItemJson();
    }

    private void ParseItemJson()
    {
        //文本在unity里面为TextAsset类型
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;//物品信息的Json格式
        JSONObject j = new JSONObject(itemsJson);
        foreach (JSONObject temp in j.list)
        {
            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            Item.Quality quality = (Item.Quality)System.Enum.Parse(typeof(Item.Quality), temp["quality"].str);
            string description = temp["description"].str;
            int capacity = (int)(temp["capacity"].n);
            int buyPrice = (int)(temp["buyPrice"].n);
            int sellPrice = (int)(temp["sellPrice"].n);
            string sprite = temp["sprite"].str;

            Item item = null; 

            string typeStr = temp["type"].str;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);

            int hp, mp;
            switch (type)   
            {
                case Item.ItemType.Consumable:
                    hp = (int)(temp["hp"].n);
                    mp = (int)(temp["mp"].n);
                    item = new Consumable(id, name, type, quality, description,capacity, buyPrice, sellPrice, hp, mp ,sprite);
                    break;
                case Item.ItemType.Equipment:
                    int atk = (int)temp["ATK"].n;
                    int def = (int)temp["DEF"].n;
                    hp = (int)temp["HP"].n;
                    mp = (int)temp["MP"].n;
                    Equipment.EquipmentType equipType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipType"].str);
                    item = new Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice,atk,def, hp, mp,equipType, sprite);
                    break;
                case Item.ItemType.Material:
                    item = new Material(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
                    break;
                case Item.ItemType.Recipe:
                    int ID1 = (int)temp["id1"].n;
                    int ID2 = (int)temp["id2"].n;
                    int ID3 = (int)temp["id3"].n;
                    int spawnID = (int)temp["spawnID"].n;
                    int amount1 = (int)temp["amount1"].n;
                    int amount2 = (int)temp["amount2"].n;
                    int amount3 = (int)temp["amount3"].n;
                    item = new Recipe(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, ID1, ID2, ID3, amount1, amount2, amount3,spawnID);
                    break;
                default:
                    break;
            }
           itemList.Add(item);
        }
    }

    public Item GetItemById(int id)
    {
        return id>itemList.Count? null: itemList[id];
    }

}
