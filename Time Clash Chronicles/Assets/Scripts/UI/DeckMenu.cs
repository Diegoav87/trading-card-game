using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // Add this using directive
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class DeckSelectManager : MonoBehaviour
{
    public GameObject[] leaderCardPrefabs; // Array of leader prefabs
    public GameObject[] cardPrefabs; // Array of card prefabs
    public Transform leaderCardGrid;
    public Transform[] cardGrids; // Array of card grids
    public GameObject deckSelectionMenu;
    public TextMeshProUGUI deckNameText; // Use TextMeshProUGUI for text component

    private DeckData currentDeckData;
    private int selectedDeckId = -1; // Initialize selected deck ID to -1

    // Dictionary to map leader card index to deck ID
    private Dictionary<int, int> leaderDeckIds = new Dictionary<int, int>();

    private GameObject hoveredCard;
    private Vector3 originalCardScale;

    void Start()
    {
        // Initialize leader deck IDs
        for (int i = 0; i < leaderCardPrefabs.Length; i++)
        {
            leaderDeckIds.Add(i + 1, -1); // Initialize deck IDs to -1
        }

        // Ensure that the deck selection menu is initially hidden
        deckSelectionMenu.SetActive(false);
    }

    public void SetLeaderCardDeckId(int leaderIndex, int deckId)
    {
        // Set the deck ID for the leader card at the specified index
        leaderDeckIds[leaderIndex + 1] = deckId; // Add 1 to leader index to match deck ID
    }

    public void OnLeaderCardClicked(int leaderIndex)
    {
        // Check if a deck is selected for the clicked leader
        int deckId;
        if (leaderDeckIds.TryGetValue(leaderIndex + 1, out deckId))
        {
            if (deckId != -1)
            {
                selectedDeckId = deckId; // Assign the selected deck ID
                StartCoroutine(GetDeck("http://localhost:5000/api/decks/", deckId));
            }
            else
            {
                Debug.LogWarning("Deck ID not set for selected leader.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid leader index.");
        }
    }

    IEnumerator GetDeck(string endpoint, int deckId)
    {
        // Make API request to get deck data
        using (UnityWebRequest request = UnityWebRequest.Get(endpoint + deckId))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch deck data from API: " + request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                currentDeckData = JsonConvert.DeserializeObject<DeckData>(json);
                PopulateDeckCards();
                deckSelectionMenu.SetActive(false); // Hide deck selection menu
            }
        }
    }

    void PopulateDeckCards()
    {
        deckNameText.text = currentDeckData.name; // Update deck name text

        for (int i = 0; i < cardGrids.Length; i++)
        {
            Transform cardGrid = cardGrids[i];
            foreach (Transform child in cardGrid)
            {
                Destroy(child.gameObject);
            }

            foreach (CardData cardData in currentDeckData.cards)
            {
                GameObject cardPrefab = cardPrefabs[i];
                GameObject cardObject = Instantiate(cardPrefab, cardGrid);
                CardViewDisplay cardDisplay = cardObject.GetComponent<CardViewDisplay>();
                if (cardDisplay != null)
                {
                    cardDisplay.LoadCard(cardData);
                }
            }
        }
    }

    // Hover functionality
    public void OnCardHoverEnter(GameObject card)
    {
        hoveredCard = card;
        originalCardScale = card.transform.localScale;
        card.transform.localScale *= 1.1f; // Increase scale by 10%
    }

    public void OnCardHoverExit()
    {
        if (hoveredCard != null)
        {
            hoveredCard.transform.localScale = originalCardScale;
            hoveredCard = null;
        }
    }
}