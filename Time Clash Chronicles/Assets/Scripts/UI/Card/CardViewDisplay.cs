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

    public Image image;

    public Image flag;

    public TMP_Text ability;

    AbilityManager abilityManager;

    void Awake()
    {
        abilityManager = FindObjectOfType<AbilityManager>();
    }

    // Load card data into each ui element
    // If the card has an ability update the description
    public void LoadCard(CardData cardData, int leaderId)
    {
        nameText.text = cardData.name;
        healthText.text = cardData.health.ToString();
        attackText.text = cardData.attack.ToString();
        costText.text = cardData.cost.ToString();
        image.sprite = Resources.Load<Sprite>("Images/CardsImages/" + cardData.card_id);
        flag.sprite = Resources.Load<Sprite>("Images/Leaders/Flags/" + leaderId);

        if (cardData.ability != null)
        {
            string description = cardData.ability.description;
            int value = abilityManager.GetAbilityValue(cardData.ability.ability_id, cardData.card_id);

            ability.text = description.Replace("{value}", value.ToString());
        }
        else
        {
            ability.text = "";
        }
    }
}