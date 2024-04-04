using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArenaSlot : MonoBehaviour, IDropHandler
{

    ArenaManager arenaManager;

    void Start()
    {
        arenaManager = FindObjectOfType<ArenaManager>();

    }
    void SetCard(GameObject card)
    {
        card.transform.SetParent(transform);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!HasCard())
        {
            GameObject dropped = eventData.pointerDrag;
            CardDragDrop draggableCard = dropped.GetComponent<CardDragDrop>();


            draggableCard.parentAfterDrag = transform;
            SetCard(dropped);
        }

    }

    public bool IsEnemySlot()
    {
        return arenaManager.enemySlots.Contains(this);
    }

    public bool HasCard()
    {
        return transform.childCount != 0;
    }

}
