using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] Deck playerDeck;
    [SerializeField] Deck enemyDeck;
    [SerializeField] Hand playerHand;
    [SerializeField] Hand enemyHand;
    [SerializeField] GameObject playerLeaderSlot;
    [SerializeField] GameObject enemyLeaderSlot;
    [SerializeField] GameObject leaderPrefab;
    [SerializeField] GameObject cardPrefab;

    [HideInInspector] public GameObject playerLeader;
    [HideInInspector] public GameObject enemyLeader;

    void Start()
    {
        CreateLeaders();
        CreateCards();
        ShuffleDecks();
        DrawCards();
    }

    void CreateLeaders()
    {
        Leader playerLeaderData = new Leader("Napoleon", "Emperador de francia", 10, Resources.Load<Sprite>("Images/leader1"));
        GameObject playerLeaderObject = Instantiate(leaderPrefab, playerLeaderSlot.transform);
        playerLeaderObject.GetComponent<LeaderController>().SetLeaderData(playerLeaderData);
        playerLeaderObject.GetComponent<LeaderDisplay>().LoadLeader(playerLeaderData);
        playerLeader = playerLeaderObject;

        Leader enemyLeaderData = new Leader("Julius", "Emperador de roma", 10, Resources.Load<Sprite>("Images/leader1"));
        GameObject enemyLeaderObject = Instantiate(leaderPrefab, enemyLeaderSlot.transform);
        enemyLeaderObject.GetComponent<LeaderController>().SetLeaderData(enemyLeaderData);
        enemyLeaderObject.GetComponent<LeaderDisplay>().LoadLeader(enemyLeaderData);
        enemyLeader = enemyLeaderObject;
    }

    void CreateCards()
    {
        playerDeck.AddCard(new Card(1, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        playerDeck.AddCard(new Card(2, "Caballero", "Caballero de la francia medieval", Resources.Load<Sprite>("Images/caballero_frances"), 4, 4, 4));
        playerDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));

        enemyDeck.AddCard(new Card(1, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        enemyDeck.AddCard(new Card(2, "Caballero", "Caballero de la francia medieval", Resources.Load<Sprite>("Images/caballero_frances"), 4, 4, 4));
        enemyDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
    }

    void ShuffleDecks()
    {
        playerDeck.ShuffleDeck();
        enemyDeck.ShuffleDeck();
    }

    void DrawCards()
    {
        DrawCardToHand(playerDeck, playerHand, 0, "player");
        DrawCardToHand(playerDeck, playerHand, 1, "player");
        DrawCardToHand(playerDeck, playerHand, 2, "player");
        DrawCardToHand(enemyDeck, enemyHand, 0, "enemy");
        DrawCardToHand(enemyDeck, enemyHand, 1, "enemy");
        DrawCardToHand(enemyDeck, enemyHand, 2, "enemy");
    }

    public void DrawCardToHand(Deck deck, Hand hand, int index, string owner)
    {
        Card drawnCard = deck.DrawCard();
        if (drawnCard != null)
        {
            hand.AddCard(drawnCard, index, owner);
        }
        else
        {
            Debug.Log("No more cards in the deck!");
        }
    }
}
