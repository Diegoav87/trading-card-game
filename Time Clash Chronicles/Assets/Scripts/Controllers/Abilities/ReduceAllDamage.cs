using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceAllDamage : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;

    AbilityManager abilityManager;

    public ReduceAllDamage(GameManager gManager, ArenaManager aManager, AbilityManager abManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
        abilityManager = abManager;
    }

    // Reduce the damage of all opponent cards. Can't go below 1.
    public void Execute(CardController cardController)
    {
        if (gameManager.currentPlayer == "player")
        {
            foreach (ArenaSlot slot in arenaManager.enemySlots)
            {
                if (slot.HasCard())
                {

                    CardController targetCardController = slot.GetComponentInChildren<CardController>();
                    int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);

                    if (targetCardController.attack > value)
                    {
                        targetCardController.UpdateAttack(-value);
                    }
                    else
                    {
                        targetCardController.attack = 1;
                        targetCardController.UpdateAttack(0);
                    }
                }
            }
        }
        else
        {
            foreach (ArenaSlot slot in arenaManager.playerSlots)
            {
                if (slot.HasCard())
                {
                    CardController targetCardController = slot.GetComponentInChildren<CardController>();
                    int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);

                    if (targetCardController.attack > value)
                    {
                        targetCardController.UpdateAttack(-value);
                    }
                    else
                    {
                        targetCardController.attack = 1;
                        targetCardController.UpdateAttack(0);
                    }
                }
            }
        }

    }
}
