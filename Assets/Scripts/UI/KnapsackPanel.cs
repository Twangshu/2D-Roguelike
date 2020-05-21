using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackPanel : BasePanel
{

    private void Awake()
    {
        ShowEvent = EventDefine.ShowKnapsackPanel; HideEvent = EventDefine.HideKnapsackPanel;
    }
}
