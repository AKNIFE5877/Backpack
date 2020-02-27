using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text toolTipText;
    private Text contentText;
    private CanvasGroup canvasGroup;

    public  float targetAlpha = 0;
    public float smoothing = 0.5f;
    private void Start()
    {
        toolTipText = GetComponent<Text>() ;
        contentText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    float last = 0;
    private void Update()
    {
        
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, (smoothing*Time.deltaTime )/ Mathf.Abs(targetAlpha - canvasGroup.alpha));
        }
    }
    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }
    public void Hide()
    {
        targetAlpha = 0;
    }
    public  void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
