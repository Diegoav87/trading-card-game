using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArenaSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            CardDragDrop draggableCard = dropped.GetComponent<CardDragDrop>();
            draggableCard.parentAfterDrag = transform;
        }

    }
}
