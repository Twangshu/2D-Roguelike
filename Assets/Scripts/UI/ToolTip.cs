using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToolTip : MonoBehaviour {


    private Text toolTipText;
    private Text contentText;
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float targetAlpha = 0;
    public float smoothing = 3;
	// Use this for initialization
	void Start () {
        toolTipText = GetComponent<Text>();
        contentText = GameObject.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
        ChangeAlpha();
    }
    public void Hide()
    {
        targetAlpha = 0;
        ChangeAlpha();
    }
    public void HideImmediately()
    {
        targetAlpha = 0;
        canvasGroup.alpha = 0;
        ChangeAlpha();
    }
    private void ChangeAlpha()
    {
            Tween tweener= DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, targetAlpha, 0.2f);
            tweener.SetUpdate(true);
        
    }
}
