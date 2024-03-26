using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerClickHandler
{

    private bool isSelected = false;
    public Image selectionHighlight;
    public ArenaManager arenaManager;



    void Start()
    {
        selectionHighlight.enabled = false;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelection();

        if (isSelected)
        {
            bool hasCardInFront = arenaManager.HasCardInFront(transform.parent.GetSiblingIndex());

            if (hasCardInFront)
            {
                Debug.Log("Attackl");
            }
            else
            {
                Debug.Log("no attack");

            }
        }
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        selectionHighlight.enabled = isSelected;
    }
}
