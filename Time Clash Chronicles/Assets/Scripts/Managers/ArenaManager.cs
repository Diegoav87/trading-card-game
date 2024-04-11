using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArenaManager : MonoBehaviour
{
    public List<ArenaSlot> playerSlots = new List<ArenaSlot>();
    public List<ArenaSlot> enemySlots = new List<ArenaSlot>();

    [HideInInspector] public CardController selectedAttacker;

    public void SetSelectedAttacker(CardController attacker)
    {
        if (attacker != null)
        {
            selectedAttacker = attacker;
        }
        else
        {
            selectedAttacker = null;
        }

    }

    public bool SlotsAreEmpty(List<ArenaSlot> slots)
    {
        foreach (ArenaSlot slot in slots)
        {
            if (slot.HasCard())
            {
                return false;
            }
        }
        return true;

    }

    public bool PlayerSlotsAreEmpty()
    {
        return SlotsAreEmpty(playerSlots);
    }

    public bool EnemySlotsAreEmpty()
    {
        return SlotsAreEmpty(enemySlots);
    }

    void ResetAttacks(List<ArenaSlot> slots)
    {
        foreach (ArenaSlot slot in slots)
        {
            if (slot.HasCard())
            {
                CardController cardController = slot.GetComponentInChildren<CardController>();

                cardController.hasAttacked = false;
            }
        }
    }

    public void ResetPlayerAttacks()
    {
        ResetAttacks(playerSlots);
    }

    public void ResetEnemyAttacks()
    {
        ResetAttacks(enemySlots);
    }

}