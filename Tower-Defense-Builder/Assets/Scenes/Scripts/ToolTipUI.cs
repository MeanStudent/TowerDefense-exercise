using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance;
    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshProUGUI;
    private RectTransform backgroundRectTransform;
    private TooltipTimer tooltipTimer;

    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("BackGroundImage").GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();
        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = rectTransform.anchoredPosition = 
            Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        textMeshProUGUI.SetText(tooltipText);
        textMeshProUGUI.ForceMeshUpdate();

        Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
        Vector2 pedding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize+pedding;
    }

    public void Show(string tooltipText,TooltipTimer tooltipTimer = null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float timer;
    }
}
