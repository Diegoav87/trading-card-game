using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArenaManager : MonoBehaviour
{
    public List<CardController> playerCards = new List<CardController>(); // Cards in player's field
    public List<CardController> enemyCards = new List<CardController>(); // Cards in enemy's field

    // Method to check if there's a card in front in the enemy's field
    public bool HasCardInFront(CardController card)
    {
        // Implement your logic here
        // For example, you can check if there's a card in front of the specified card
        return false;
    }
}