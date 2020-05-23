using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : BasePanel
{
    private void Awake()
    {
        ShowEvent = EventDefine.ShowShopPanel; HideEvent = EventDefine.HideShopPanel;
    }
}
