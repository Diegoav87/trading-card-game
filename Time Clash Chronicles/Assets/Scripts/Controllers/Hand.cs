using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public List<GameObject> handSlots = new List<GameObject>();
    public GameObject handSlotPrefab;
    public GameObject cardPrefab;
    public Transform handParent;

    public void AddCard(Card card, int index, string owner)
    {
        cards.Add(card);

        GameObject cardObject = Instantiate(cardPrefab, handSlots[index].transform);
        cardObject.GetComponent<CardDisplay>().LoadCard(card);
        cardObject.GetComponent<CardController>().SetCardData(card);
        cardObject.GetComponent<CardController>().owner = owner;
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
