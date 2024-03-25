using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Deck deck;
    public Hand hand;


    void Start()
    {

        deck.ShuffleDeck();
        hand.AddCard(deck.cards[0]);
        hand.AddCard(deck.cards[0]);
    }

    public void DrawCardToHand()
    {
        Card drawnCard = deck.DrawCard();
        if (drawnCard != null)
        {
            hand.AddCard(drawnCard);
        }
        else
        {
            Debug.Log("No more cards in the deck!");
        }
    }
}
