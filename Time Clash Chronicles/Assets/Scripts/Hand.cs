using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public GameObject handSlotPrefab;
    public GameObject cardPrefab;
    public Transform handParent;

    public void AddCard(Card card)
    {
        cards.Add(card);
        GameObject handSlot = Instantiate(handSlotPrefab, handParent);

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
