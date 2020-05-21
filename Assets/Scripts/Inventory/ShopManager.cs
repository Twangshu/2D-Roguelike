using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    private static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public Dictionary<int, int[]> chestDrop = new Dictionary<int, int[]>();

    private void Awake()
    {
        _instance = this;
        Init();//初始化宝箱掉落物品和对应关卡的关系

    }
    private void Init()
    {
        chestDrop.Add(1, new int[] { 1, 2, 3,4,5,6,7,8,9,10 });
        chestDrop.Add(6, new int[] { 4, 2, 3 });
        chestDrop.Add(11, new int[] { 4, 5, 3 });
        chestDrop.Add(16, new int[] { 5, 6, 4 });
        chestDrop.Add(21, new int[] { 7, 6, 5 });
    }
    }
