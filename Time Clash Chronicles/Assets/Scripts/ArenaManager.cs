using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArenaManager : MonoBehaviour
{
    public List<ArenaSlot> playerSlots = new List<ArenaSlot>();
    public List<ArenaSlot> enemySlots = new List<ArenaSlot>();

    public CardController selectedAttacker;

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



}