using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessagePanel : MonoBehaviour {

    private  Text message1;
    private  Text message2;
    private  Text message3;
    private CanvasGroup canvasGroup;
    private bool isHiding = false;
    private float time = 4f;
    private float timer = 0;

    private List<string> messageStr = new List<string>();
    private void Awake()
    {
        message1 = transform.Find("message1").GetComponent<Text>();
        message2 = transform.Find("message2").GetComponent<Text>();
        message3 = transform.Find("message3").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        EventCenter.AddListener<string>(EventDefine.ShowMessage, ShowMessage);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > time)
            Hide();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventDefine.ShowMessage, ShowMessage);
    }
    private void ShowMessage(string msg)
    {

        if(messageStr.Count==0)
        {
            messageStr.Add(msg);
            UpdateMessage();
        }
        else if(messageStr.Count==1)
        {
            messageStr.Add(msg);
            UpdateMessage();
        }
        else if(messageStr.Count==2)
        {
            messageStr.Add(msg);
            UpdateMessage();
        }
        else
        {
            messageStr.RemoveAt(0);
            messageStr.Add(msg);
            UpdateMessage();
        }
    }
    private void UpdateMessage()
    {
        timer = 0;
        canvasGroup.alpha = 1;
        message1.text = messageStr[0];
        if(messageStr.Count>1)
            message2.text = messageStr[1];
        if (messageStr.Count>2)
            message3.text = messageStr[2];
    }
    private void Hide()
    {
        canvasGroup.alpha = 0;
    }
}
