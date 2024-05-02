using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [HideInInspector] public bool isHealingAbilityActive;

    [HideInInspector] public bool isIncreaseDamageAbilityActive;

    // Call the execute method of the card ability
    public void ActivateAbility(CardAbility ability, CardController cardController)
    {
        if (ability != null)
        {

            ability.Execute(cardController);
        }
    }

    // Get the ability execution value for each card
    public int GetAbilityValue(int ability_id, int card_id)
    {
        int value = 0;

        if (ability_id == 1)
        {
            if (card_id == 3)
            {
                value = 3;
            }
            else if (card_id == 4)
            {
                value = 1;
            }
            else if (card_id == 6)
            {
                value = 2;
            }
        }
        else if (ability_id == 2)
        {
            if (card_id == 11)
            {
                value = 3;
            }
            else if (card_id == 9)
            {
                value = 2;
            }
            else if (card_id == 10)
            {
                value = 1;
            }
        }
        else if (ability_id == 3)
        {
            if (card_id == 16)
            {
                value = 1;
            }
            else if (card_id == 19)
            {
                value = 3;
            }
            else if (card_id == 20)
            {
                value = 2;
            }
        }
        else if (ability_id == 4)
        {
            if (card_id == 26)
            {
                value = 1;
            }
            else if (card_id == 24)
            {
                value = 2;
            }
            else if (card_id == 27)
            {
                value = 3;
            }
        }
        else if (ability_id == 5)
        {
            if (card_id == 31)
            {
                value = 3;
            }
            else if (card_id == 33)
            {
                value = 1;
            }
            else if (card_id == 34)
            {
                value = 2;
            }
        }

        return value;
    }



}
