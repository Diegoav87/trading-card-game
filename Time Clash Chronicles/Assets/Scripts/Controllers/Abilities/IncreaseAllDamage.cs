using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAllDamage : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;
    AbilityManager abilityManager;

    public IncreaseAllDamage(GameManager gManager, ArenaManager aManager, AbilityManager abManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
        abilityManager = abManager;
    }

    // Increase the damage of all ally cards. Goes up to 9 max.
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

                    if (targetCardController.attack < 10 - value)
                    {
                        targetCardController.UpdateAttack(value);
                    }
                    else
                    {
                        targetCardController.attack = 9;
                        targetCardController.UpdateAttack(0);
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

                    if (targetCardController.attack < 10 - value)
                    {
                        targetCardController.UpdateAttack(value);
                    }
                    else
                    {
                        targetCardController.attack = 9;
                        targetCardController.UpdateAttack(0);
                    }
                }
            }
        }

    }
}
