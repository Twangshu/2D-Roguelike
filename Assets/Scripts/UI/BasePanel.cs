using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Vector3 initPos;
    protected EventDefine ShowEvent;
    protected EventDefine HideEvent;

    public virtual void Start()
    {
        initPos = transform.position;
        HideThis();
        EventCenter.AddListener(ShowEvent, ShowThis);
        EventCenter.AddListener(HideEvent, HideThis);
    }

    public virtual void OnDestroy()
    {
        EventCenter.RemoveListener(ShowEvent, ShowThis);
        EventCenter.RemoveListener(HideEvent, HideThis);
    }

    public virtual void ShowThis()
    {
        transform.position = initPos;
    }

    public virtual void HideThis()
    {
        transform.position = new Vector3(1000, 1000, 1000);
    }

}
