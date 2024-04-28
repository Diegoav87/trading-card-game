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

    public void InvokeCard()
    {
        GameObject selectedCardObject = enemyHand.SelectCardFromEnemyHand();

        if (selectedCardObject != null)
        {
            CardController selectedCardController = selectedCardObject.GetComponent<CardController>();
            arenaManager.InvokeCardIntoArena(selectedCardObject, selectedCardController);
        }
    }

    public IEnumerator ActivateAbility()
    {
        ArenaSlot enemySlot = arenaManager.GetRandomOccupiedSlot(arenaManager.enemySlots);

        CardController enemyCardController = enemySlot.GetComponentInChildren<CardController>();

        if (!enemyCardController.hasUsedAbility)
        {
            enemyCardController.SelectCard();

        }

        yield return new WaitForSeconds(1f);
        gameManager.HandleAbilityActivation();
    }

    public IEnumerator AttackCard()
    {

        ArenaSlot occupiedEnemySlot = arenaManager.GetRandomOccupiedSlot(arenaManager.enemySlots);
        CardController enemyCardController = occupiedEnemySlot.GetComponentInChildren<CardController>();

        enemyCardController.SelectCard();

        yield return new WaitForSeconds(1f);

        arenaManager.AttackPlayerCard();

    }
}
