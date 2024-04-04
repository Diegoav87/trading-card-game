using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Card> cards = new List<Card>();
    [SerializeField] List<GameObject> handSlots = new List<GameObject>();
    [SerializeField] GameObject handSlotPrefab;
    [SerializeField] GameObject cardPrefab;

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
