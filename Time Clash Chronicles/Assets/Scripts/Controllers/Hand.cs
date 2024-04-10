using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    [SerializeField] List<GameObject> handSlots = new List<GameObject>();
    [SerializeField] GameObject handSlotPrefab;
    [SerializeField] GameObject cardPrefab;

    public void AddCard(Card card, string owner)
    {
        GameObject emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            cards.Add(card);

            GameObject cardObject = Instantiate(cardPrefab, emptySlot.transform);
            cardObject.GetComponent<CardDisplay>().LoadCard(card);
            cardObject.GetComponent<CardController>().SetCardData(card);
            cardObject.GetComponent<CardController>().owner = owner;
        }
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

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }

    void ClearHand()
    {
        cards.Clear();
    }
}
