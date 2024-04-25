using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    [SerializeField] List<GameObject> handSlots = new List<GameObject>();
    [SerializeField] GameObject handSlotPrefab;
    [SerializeField] GameObject cardPrefab;



    int handSize = 6;

    public void AddCard(Card card, string owner)
    {
        GameObject emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            cards.Add(card);

            GameObject cardObject = Instantiate(cardPrefab, emptySlot.transform);
            cardObject.GetComponent<CardDisplay>().LoadCard(card);
            cardObject.GetComponent<CardController>().owner = owner;
            cardObject.GetComponent<CardController>().SetCardData(card);
        }
    }

    bool IsHandFull()
    {
        return cards.Count >= handSize;
    }

    GameObject GetEmptySlot()
    {
        foreach (GameObject slot in handSlots)
        {
            if (!slot.GetComponent<HandSlot>().HasCard())
            {
                return slot;
            }
        }

        return null;
    }

    public Tuple<Card, GameObject> SelectCardFromEnemyHand()
    {
        Tuple<Card, GameObject> selectedCardTuple = null;

        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            if (GameManager.Instance.enemyCoins.coins >= card.cost)
            {
                GameObject cardObject = handSlots[i].GetComponentInChildren<CardController>().gameObject;
                selectedCardTuple = Tuple.Create(card, cardObject);
                break;
            }
        }

        return selectedCardTuple;
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }
}
