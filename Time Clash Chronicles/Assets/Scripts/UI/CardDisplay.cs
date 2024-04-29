using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text attackText;
    public TMP_Text costText;
    public Image imageSprite;

    public Image flag;

    public TMP_Text ability;

    AbilityManager abilityManager;

    void Awake()
    {
        abilityManager = FindObjectOfType<AbilityManager>();
    }

    public void LoadCard(Card card)
    {
        nameText.text = card.name;
        healthText.text = card.health.ToString();
        attackText.text = card.attack.ToString();
        costText.text = card.cost.ToString();
        imageSprite.sprite = card.image;
        flag.sprite = card.flag;

        if (card.abilityData != null)
        {
            string description = card.abilityData.description;
            int value = abilityManager.GetAbilityValue(card.abilityData.ability_id, card.id);

            ability.text = description.Replace("{value}", value.ToString());
        }
        else
        {
            ability.text = "";
        }
    }


}
