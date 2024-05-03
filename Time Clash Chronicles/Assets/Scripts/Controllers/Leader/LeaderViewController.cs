using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class LeaderViewController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject cardViewPrefab;
    [SerializeField] GameObject leaderViewPrefab;

    int deckId;

    DeckMenuController deckMenuController;

    void Start()
    {
        deckMenuController = FindObjectOfType<DeckMenuController>();
    }
    public void SetData(int id)
    {
        deckId = id;
    }

    // When clicked activate the card canvas and get the card data for the deck
    public void OnPointerClick(PointerEventData eventData)
    {
        deckMenuController.cardCanvas.SetActive(true);
        deckMenuController.currentDeckId = deckId;

        StartCoroutine(APIManager.Instance.GetDeck("decks/" + deckId + "/", (deck) =>
        {
            foreach (CardData card in deck.cards)
            {
                GameObject cardView = Instantiate(cardViewPrefab, deckMenuController.cardGrid.transform);
                cardView.GetComponent<CardViewDisplay>().LoadCard(card, deckId);
            }

            GameObject leaderView = Instantiate(leaderViewPrefab, deckMenuController.leaderSlot.transform);
            leaderView.GetComponent<LeaderViewDisplay>().LoadLeader(deck.leader.leader_id, deck.leader.name);
        }));
    }
}
