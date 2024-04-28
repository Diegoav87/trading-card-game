using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAllDamage : CardAbility
{
    GameManager gameManager;
    ArenaManager arenaManager;

    public IncreaseAllDamage(GameManager gManager, ArenaManager aManager)
    {
        gameManager = gManager;
        arenaManager = aManager;
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
                    targetCardController.UpdateAttack(2);
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
                    targetCardController.UpdateAttack(2);
                }
            }
        }

    }
}
