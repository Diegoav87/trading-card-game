using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text healthText;
    public TMP_Text attackText;
    public TMP_Text costText;
    public Image imageSprite;


    public void LoadCard(Card card)
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        healthText.text = card.health.ToString();
        attackText.text = card.attack.ToString();
        costText.text = card.cost.ToString();
        imageSprite.sprite = card.image;
    }


}
