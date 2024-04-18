using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class CardViewDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text attackText;
    public TMP_Text costText;

    public void LoadCard(CardData cardData)
    {
        nameText.text = cardData.name;
        healthText.text = cardData.health.ToString();
        attackText.text = cardData.attack.ToString();
        costText.text = cardData.cost.ToString();
    }
}