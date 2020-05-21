using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventory {

    private static Knapsack _instance;
    public static Knapsack Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Start()
    {
        base.Start();
        foreach (Slot slot in slotList)
        {
            slot.Start();
        }
        EventCenter.Broadcast(EventDefine.HideKnapsackPanel);
    }
    public bool IsFull()
    {
        foreach (Slot slot in slotList)
        {
           if( slot.gameObject.transform.childCount == 0)
            return false;
        }
        return true;
    }
}
