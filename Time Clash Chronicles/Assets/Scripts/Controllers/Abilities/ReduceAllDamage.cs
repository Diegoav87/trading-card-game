using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceAllDamage : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;

    public ReduceAllDamage(GameManager gManager, ArenaManager aManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
    }

    public void Execute(CardController cardController)
    {
        if (gameManager.currentPlayer == "player")
        {
            foreach (ArenaSlot slot in arenaManager.enemySlots)
            {
                if (slot.HasCard())
                {

                    CardController targetCardController = slot.GetComponentInChildren<CardController>();

                    if (targetCardController.attack > 1)
                    {
                        targetCardController.UpdateAttack(-2);

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

                    if (targetCardController.attack > 1)
                    {
                        targetCardController.UpdateAttack(-2);

                    }
                }
            }
        }

    }
}
