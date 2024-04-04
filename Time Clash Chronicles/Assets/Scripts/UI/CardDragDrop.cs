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
    private Transform originalParent;


    ArenaManager arenaManager;

    public bool dragging = false;

    void Start()
    {
        parentAfterDrag = transform.parent;
        originalParent = parentAfterDrag;
        arenaManager = FindObjectOfType<ArenaManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsInArena())
        {
            return;
        }

        dragging = true;
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


        dragging = false;

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
