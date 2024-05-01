using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StealAllLife : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;

    AbilityManager abilityManager;

    public StealAllLife(GameManager gManager, ArenaManager aManager, AbilityManager abManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
        abilityManager = abManager;
    }
    public void Execute(CardController cardController)
    {
        if (gameManager.currentPlayer == "player")
        {
            int count = 0;

            int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);

            foreach (ArenaSlot slot in arenaManager.enemySlots)
            {
                if (slot.HasCard())
                {
                    count += 1;

                    CardController targetCardController = slot.GetComponentInChildren<CardController>();

                    targetCardController.UpdateHealth(-value);

                    if (targetCardController.health <= 0)
                    {
                        UnityEngine.Object.Destroy(targetCardController.gameObject);
                    }
                }
            }

            if (cardController.health < 10 - (count * value))
            {
                cardController.UpdateHealth(count * value);
            }
            else
            {
                cardController.health = 9;
                cardController.UpdateHealth(0);
            }
        }
        else
        {
            int count = 0;

            int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);

            foreach (ArenaSlot slot in arenaManager.playerSlots)
            {
                if (slot.HasCard())
                {
                    count += 1;

                    CardController targetCardController = slot.GetComponentInChildren<CardController>();

                    targetCardController.UpdateHealth(-value);

                    if (targetCardController.health <= 0)
                    {
                        UnityEngine.Object.Destroy(targetCardController.gameObject);
                    }
                }
            }

            if (cardController.health < 10 - (count * value))
            {
                cardController.UpdateHealth(count * value);
            }
            else
            {
                cardController.health = 9;
                cardController.UpdateHealth(0);
            }
        }

    }
}
