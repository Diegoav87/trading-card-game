using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand : MonoBehaviour
{
    [SerializeField] List<GameObject> handSlots = new List<GameObject>();
    [SerializeField] GameObject handSlotPrefab;
    [SerializeField] GameObject cardPrefab;

    GameManager gameManager;

    [HideInInspector] public List<Card> cards = new List<Card>();


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Instantiate a card in the scene and load its data and set it up
    public void AddCard(Card card, string owner)
    {
        GameObject emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            cards.Add(card);

            GameObject cardObject = Instantiate(cardPrefab, emptySlot.transform);
            cardObject.GetComponent<CardDisplay>().LoadCard(card);

            CardController cardController = cardObject.GetComponent<CardController>();
            cardController.owner = owner;
            cardController.SetCardData(card);
            cardController.FlipCard();
            cardObject.GetComponent<CardHover>().cardData = card;

            if (owner == "enemy")
            {
                cardController.FlipCard();
            }
        }
    }

    // Get an empty slot in the hand slots if there are any
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

    // Select a card from the enemy hand if the enemy has enough coins to inovke it
    public GameObject SelectCardFromEnemyHand()
    {
        foreach (GameObject slot in handSlots)
        {
            if (slot.GetComponent<HandSlot>().HasCard())
            {
                CardController cardController = slot.GetComponentInChildren<CardController>();
                GameObject cardObject = cardController.gameObject;
                if (gameManager.enemyCoins.coins >= cardController.cost)
                {
                    return cardObject;

                }
            }
        }

        return null;
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }
}
