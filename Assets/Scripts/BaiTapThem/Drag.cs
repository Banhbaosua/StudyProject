using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    private float OriginalX, OriginalY;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On pointer down");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag");
        rectTransform.anchoredPosition +=eventData.delta/canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On begin drag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag End");
        canvasGroup.blocksRaycasts = true;
        if(rectTransform.anchoredPosition == image.rectTransform.anchoredPosition)
            GetComponent<Image>().raycastTarget = false;
        else if(rectTransform.anchoredPosition != image.rectTransform.anchoredPosition)
            rectTransform.anchoredPosition = new Vector2(OriginalX,OriginalY);
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        OriginalX = rectTransform.anchoredPosition.x;
        OriginalY = rectTransform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
