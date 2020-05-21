using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    private ToolTip toolTip;
    private ItemUI pickedItem;
    private bool isPickingItem = false;
    public ItemUI PickedItem { get => pickedItem; set => pickedItem = value; }
    public bool IsPickingItem { get => isPickingItem; set => isPickingItem = value; }
    public static UIManager Instance { get => _instance; set => _instance = value; }
    public Camera UICamera { get => uICamera; set => uICamera = value; }
    [HideInInspector]
    public bool isToolTipShow = false;
    private Canvas canvas;
    [Header("提示条的偏移量")]
    public Vector2 toolTipPositonOffset = new Vector2(10, -10);

    public ScrollRect scrollRect;

    public Slot slot;
    public Transform preTrans;

    private Camera uICamera;
    [SerializeField]
    private bool isBagOn = false;//背包是否打开

    private void Start()
    {
        Instance = this;
        canvas = FindObjectOfType<Canvas>();
        toolTip = FindObjectOfType<ToolTip>();
        UICamera = transform.Find("UICamera").GetComponent<Camera>();
        PickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        PickedItem.gameObject.SetActive(false);

    }

    private void Update()
    {
        UpdateToolTip();
        if(Input.GetKeyDown(KeyCode.B))
        {
            EventDefine eventDefine = isBagOn ? EventDefine.HideKnapsackPanel : EventDefine.ShowKnapsackPanel;
            isBagOn = !isBagOn;
            EventCenter.Broadcast(eventDefine);
        }    
    }

    private void UpdateToolTip()
    {
        Vector2 position;
        if (isPickingItem)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, UICamera, out position);
            pickedItem.transform.localPosition = position;
        }
        if (isToolTipShow && !IsPickingItem)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, UICamera, out position);//控制面板跟随鼠标
            toolTip.gameObject.transform.localPosition = position + toolTipPositonOffset;
        }
        if (isToolTipShow && Input.GetKeyDown(KeyCode.B))
        {
            isToolTipShow = false;
            HideToolTip();
        }

    }
    public void ShowToolTip(string text)
    {
        if (IsPickingItem)
            return;
        toolTip.Show(text);
    }
    public void HideToolTip()
    {
        toolTip.Hide();
    }

    public void PickUpItem(Item item,int amount)
    {
        PickedItem.SetPickingItem(item,amount);
        PickedItem.Show();
        IsPickingItem = true;
        toolTip.HideImmediately();
    }

    public void RemoveItem(int amount = 1)
    {
        PickedItem.ReduceAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            IsPickingItem = false;
            scrollRect.enabled = true;
            PickedItem.Hide();

        }
    }
}
