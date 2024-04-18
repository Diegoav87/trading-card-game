using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class Tooltip : MonoBehaviour
{
    [SerializeField] TMP_Text tooltipText;

    void Start()
    {
        gameObject.SetActive(false);
        ShowTooltip();
    }


    void ShowTooltip()
    {
        gameObject.SetActive(true);

        tooltipText.text = "Hello";

    }
}
