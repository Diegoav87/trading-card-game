using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealAllLife : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;

    public StealAllLife(GameManager gManager, ArenaManager aManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
    }
    public void Execute(CardController cardController)
    {
        if (gameManager.currentPlayer == "player")
        {
            int count = 0;

            foreach (ArenaSlot slot in arenaManager.enemySlots)
            {
                if (slot.HasCard())
                {
                    count += 1;

                    CardController targetCardController = slot.GetComponentInChildren<CardController>();
                    targetCardController.UpdateHealth(-1);



                    if (targetCardController.health <= 0)
                    {
                        UnityEngine.Object.Destroy(targetCardController.gameObject);
                    }
                }
            }

            cardController.UpdateHealth(count);
        }
        else
        {
            int count = 0;

            foreach (ArenaSlot slot in arenaManager.playerSlots)
            {
                if (slot.HasCard())
                {
                    count += 1;

                    CardController targetCardController = slot.GetComponentInChildren<CardController>();
                    targetCardController.UpdateHealth(-1);

                    if (targetCardController.health <= 0)
                    {
                        UnityEngine.Object.Destroy(targetCardController.gameObject);
                    }
                }
            }

            cardController.UpdateHealth(count);

        }

    }
}
