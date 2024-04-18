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
        GameObject droppedCard = eventData.pointerDrag;
        CardController cardController = droppedCard.GetComponent<CardController>();
        CardDragDrop cardDrag = droppedCard.GetComponent<CardDragDrop>();

        if (!HasCard() && cardController.CanInvoke())
        {
            cardDrag.parentAfterDrag = transform;
            SetCard(droppedCard);
            cardController.InvokeCard();
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
