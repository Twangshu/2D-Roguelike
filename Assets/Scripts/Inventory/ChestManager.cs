using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ChestManager : MonoBehaviour {
    private static ChestManager _instance;
    public static ChestManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public Dictionary<int, int[]> chestDrop = new Dictionary<int, int[]>();
    public  GameObject item;

    private void Awake()
    {
        _instance = this;
        item = Resources.Load("Prefebs/Item/dropItem") as GameObject;
        Init();//初始化宝箱掉落物品和对应关卡的关系
        
    }
    private void Init()
    {
        chestDrop.Add(1, new int[] { 1, 2, 3 });
        chestDrop.Add(2, new int[] { 4, 2, 3 });
        chestDrop.Add(3, new int[] { 4, 5, 3 });
        chestDrop.Add(4, new int[] { 5, 6, 4 });
        chestDrop.Add(5, new int[] { 7, 6, 5 });
    }
}
