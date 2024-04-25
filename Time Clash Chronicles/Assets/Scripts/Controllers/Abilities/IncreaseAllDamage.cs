using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAllDamage : CardAbility
{
    public void Execute(CardController cardController)
    {
        if (GameManager.Instance.currentPlayer == "player")
        {
            foreach (ArenaSlot slot in ArenaManager.Instance.playerSlots)
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
            foreach (ArenaSlot slot in ArenaManager.Instance.enemySlots)
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
