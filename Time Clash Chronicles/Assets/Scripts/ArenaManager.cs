using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArenaManager : MonoBehaviour
{
    public List<CardController> playerCards = new List<CardController>();
    public List<CardController> enemyCards = new List<CardController>();

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