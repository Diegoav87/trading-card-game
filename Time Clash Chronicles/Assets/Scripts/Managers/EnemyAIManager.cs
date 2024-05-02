using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] Hand enemyHand;

    GameManager gameManager;
    ArenaManager arenaManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        arenaManager = FindObjectOfType<ArenaManager>();
    }

    // Select a card from the enemy's hand if there are availablev and invoke it into the arena
    public void InvokeCard()
    {
        GameObject selectedCardObject = enemyHand.SelectCardFromEnemyHand();

        if (selectedCardObject != null)
        {
            CardController selectedCardController = selectedCardObject.GetComponent<CardController>();
            arenaManager.InvokeCardIntoArena(selectedCardObject, selectedCardController);
        }
    }

    // Get a random enemy card from the arena and activate its ability
    public IEnumerator ActivateAbility()
    {
        ArenaSlot enemySlot = arenaManager.GetRandomOccupiedSlot(arenaManager.enemySlots);

        CardController enemyCardController = enemySlot.GetComponentInChildren<CardController>();

        if (!enemyCardController.hasUsedAbility && enemyCardController.cardData.ability != null)
        {
            enemyCardController.SelectCard();
            yield return new WaitForSeconds(1f);
            gameManager.HandleAbilityActivation();
        }
    }

    // Select a random enemy card from the arena and attack a player card
    public IEnumerator AttackCard()
    {
        ArenaSlot occupiedEnemySlot = arenaManager.GetRandomOccupiedSlot(arenaManager.enemySlots);
        CardController enemyCardController = occupiedEnemySlot.GetComponentInChildren<CardController>();

        if (!enemyCardController.hasAttacked)
        {
            enemyCardController.SelectCard();

            yield return new WaitForSeconds(1f);

            arenaManager.AttackPlayerCard();
        }


    }
}
