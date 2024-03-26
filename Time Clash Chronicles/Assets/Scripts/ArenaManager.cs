using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public List<ArenaSlot> playerSlots = new List<ArenaSlot>();
    public List<ArenaSlot> enemySlots = new List<ArenaSlot>();

    public bool HasCardInFront(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < enemySlots.Count - 1)
        {
            return enemySlots[slotIndex].HasCard();
        }

        return false;
    }
}
