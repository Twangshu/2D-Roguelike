using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    ///单列模式
    /// </summary>
    private static GameManager _instance; //字段
    public static GameManager Instance { get => _instance; set => _instance = value; } //字段的属性
    


    private void Awake()
    {
        Instance = this;
    }
}
