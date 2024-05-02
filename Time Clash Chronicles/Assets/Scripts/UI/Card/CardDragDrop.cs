using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image image;
    [SerializeField] Image cardBackground;
    [HideInInspector] public Transform parentAfterDrag;

    Transform originalParent;

    CardController cardController;

    void Start()
    {
        parentAfterDrag = transform.parent;
        originalParent = parentAfterDrag;
        cardController = GetComponent<CardController>();
    }

    // When the card drag begins set this card on top of the ui hierarchy
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cardController.CanInvoke())
        {
            parentAfterDrag = transform.parent;

            transform.SetAsLastSibling();
            image.raycastTarget = false;
            cardBackground.raycastTarget = false;
            transform.position = Input.mousePosition;
        }


    }

    // When the card is being dragged change its position
    public void OnDrag(PointerEventData eventData)
    {
        if (cardController.CanInvoke())
        {
            transform.position = Input.mousePosition;
        }
    }

    // When the drag ends check if the card is on top of an arena slot
    // If the slot is not from the opponent drop the card into the slot
    public void OnEndDrag(PointerEventData eventData)
    {

        ArenaSlot arenaSlot = eventData.pointerEnter.GetComponent<ArenaSlot>();

        if (arenaSlot != null)
        {
            if ((arenaSlot.IsEnemySlot() && GetComponent<CardController>().owner == "player") || (!arenaSlot.IsEnemySlot() && GetComponent<CardController>().owner == "enemy"))
            {
                transform.SetParent(originalParent);
            }
        }

        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
        cardBackground.raycastTarget = true;

    }
}
