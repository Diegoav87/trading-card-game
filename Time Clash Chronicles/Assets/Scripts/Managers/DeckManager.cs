using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Deck playerDeck;
    public Deck enemyDeck;
    public Hand playerHand;
    public Hand enemyHand;
    public GameObject leaderSlot;

    public GameObject leaderPrefab;



    public GameObject cardPrefab;


    void Start()
    {
        Leader playerLeaderData = new Leader("Napoleon", "Emperador de francia", 10, Resources.Load<Sprite>("Images/leader1"));
        GameObject playerLeaderObject = Instantiate(leaderPrefab, leaderSlot.transform);
        playerLeaderObject.GetComponent<LeaderController>().SetLeaderData(playerLeaderData);
        playerLeaderObject.GetComponent<LeaderDisplay>().LoadLeader(playerLeaderData);

        playerDeck.AddCard(new Card(1, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        playerDeck.AddCard(new Card(2, "Caballero", "Caballero de la francia medieval", Resources.Load<Sprite>("Images/caballero_frances"), 4, 4, 4));
        playerDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));

        enemyDeck.AddCard(new Card(1, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        enemyDeck.AddCard(new Card(2, "Caballero", "Caballero de la francia medieval", Resources.Load<Sprite>("Images/caballero_frances"), 4, 4, 4));
        enemyDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));

        playerDeck.ShuffleDeck();
        enemyDeck.ShuffleDeck();


        DrawCardToHand(playerDeck, playerHand, 0);
        DrawCardToHand(playerDeck, playerHand, 1);
        DrawCardToHand(playerDeck, playerHand, 2);
        DrawCardToHand(enemyDeck, enemyHand, 0);
        DrawCardToHand(enemyDeck, enemyHand, 1);
        DrawCardToHand(enemyDeck, enemyHand, 2);
    }

    public void DrawCardToHand(Deck deck, Hand hand, int index)
    {
        Card drawnCard = deck.DrawCard();
        if (drawnCard != null)
        {
            hand.AddCard(drawnCard, index);
        }
        else
        {
            Debug.Log("No more cards in the deck!");
        }
    }
}
