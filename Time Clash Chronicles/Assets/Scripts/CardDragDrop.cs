using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public Image cardBackground;

    public bool dragging = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        parentAfterDrag = transform.parent;

        transform.SetAsLastSibling();
        image.raycastTarget = false;
        cardBackground.raycastTarget = false;
        transform.position = Input.mousePosition;

        CardHover cardHoverScript = GetComponent<CardHover>();

        if (cardHoverScript != null)
        {
            cardHoverScript.ResetScale();
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        cardBackground.raycastTarget = true;

    }
}
