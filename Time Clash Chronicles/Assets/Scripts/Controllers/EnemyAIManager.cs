using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] Hand enemyHand;

    public void InvokeCard()
    {
        Tuple<Card, GameObject> selectedCardTuple = enemyHand.SelectCardFromEnemyHand();

        if (selectedCardTuple != null)
        {
            Card selectedCard = selectedCardTuple.Item1;
            GameObject selectedCardObject = selectedCardTuple.Item2;

            if (GameManager.Instance.enemyCoins.coins >= selectedCard.cost)
            {
                ArenaManager.Instance.InvokeCardIntoArena(selectedCardObject, selectedCard);
            }
        }
    }

    public IEnumerator ActivateAbility()
    {
        ArenaSlot enemySlot = ArenaManager.Instance.GetRandomOccupiedSlot(ArenaManager.Instance.enemySlots);

        CardController enemyCardController = enemySlot.GetComponentInChildren<CardController>();

        if (!enemyCardController.hasUsedAbility)
        {
            enemyCardController.SelectCard();

        }

        yield return new WaitForSeconds(1f);
        GameManager.Instance.HandleAbilityActivation();
    }

    public void AttackCard()
    {

        StartCoroutine(ArenaManager.Instance.AttackPlayerCard());

    }
}
