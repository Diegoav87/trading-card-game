using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] Image image;
    [SerializeField] Image cardBackground;
    Transform originalParent;

    void Start()
    {
        parentAfterDrag = transform.parent;
        originalParent = parentAfterDrag;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsInArena())
        {
            return;
        }

        parentAfterDrag = transform.parent;

        transform.SetAsLastSibling();
        image.raycastTarget = false;
        cardBackground.raycastTarget = false;
        transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsInArena())
        {
            return;
        }

        transform.position = Input.mousePosition;
    }

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

    bool IsInArena()
    {
        return transform.parent.GetComponent<ArenaSlot>() != null;
    }
}
