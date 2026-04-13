using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    private Vector3 startPosition;
    private Transform startParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        
        
        
        canvas = GetComponentInParent<Canvas>();
        
        if (canvas == null)
        {
            Debug.LogError("Nu am gasit niciun Canvas! Asigura-te ca acest obiect este pus in interiorul unui Canvas.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        
        startPosition = transform.position;
        startParent = transform.parent;
        
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        
        if (canvas != null)
        {
            transform.SetParent(canvas.transform); 
        }
        
        itemBeingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        else
        {
            
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        itemBeingDragged = null;
        
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        
        if (canvas != null && transform.parent == canvas.transform)
        {
            
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }

    public Transform GetStartParent()
    {
        return startParent;
    }
}