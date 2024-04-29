using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAll : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;
    AbilityManager abilityManager;

    public HealAll(GameManager gManager, ArenaManager aManager, AbilityManager abManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
        abilityManager = abManager;
    }

    public void Execute(CardController cardController)
    {
        if (gameManager.currentPlayer == "player")
        {
            foreach (ArenaSlot slot in arenaManager.playerSlots)
            {
                if (slot.HasCard())
                {
                    CardController targetCardController = slot.GetComponentInChildren<CardController>();

                    int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);

                    if (targetCardController.health < 10 - value)
                    {
                        targetCardController.UpdateHealth(value);
                    }
                    else
                    {
                        targetCardController.health = 9;
                        targetCardController.UpdateHealth(0);
                    }
                }
            }
        }
        else
        {
            foreach (ArenaSlot slot in arenaManager.enemySlots)
            {
                if (slot.HasCard())
                {
                    CardController targetCardController = slot.GetComponentInChildren<CardController>();

                    int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);

                    if (targetCardController.health < 10 - value)
                    {
                        targetCardController.UpdateHealth(value);
                    }
                    else
                    {
                        targetCardController.health = 9;
                        targetCardController.UpdateHealth(0);
                    }
                }
            }
        }

    }


}
