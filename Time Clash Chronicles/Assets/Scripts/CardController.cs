using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerClickHandler
{

    private bool isSelected = false;
    public Image selectionHighlight;



    void Start()
    {
        selectionHighlight.enabled = false;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelection();
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        selectionHighlight.enabled = isSelected;
    }
}
