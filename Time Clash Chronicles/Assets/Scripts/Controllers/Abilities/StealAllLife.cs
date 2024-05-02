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

    // Steals life from all opponent cards and adds it to the current card. Goes up to 9 max.
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

                if (cardController.health > cardController.maxHealth)
                {
                    cardController.maxHealth = cardController.health;
                }
            }
            else
            {
                cardController.health = 9;
                cardController.UpdateHealth(0);
                cardController.maxHealth = 9;
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

                if (cardController.health > cardController.maxHealth)
                {
                    cardController.maxHealth = cardController.health;
                }
            }
            else
            {
                cardController.health = 9;
                cardController.UpdateHealth(0);
                cardController.maxHealth = 9;
            }
        }

    }
}
