using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;



public class GameManager : MonoBehaviour
{
    public enum TurnState
    {
        DrawPhase,
        MainPhase,
        EndPhase
    }

    string apiURL = "http://localhost:5000/api/";
    string data;

    public static GameManager Instance { get; private set; }
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

    public CoinController playerCoins;

    public CoinController enemyCoins;

    public TurnState currentTurnState;
    public Button endTurnButton;

    public string currentPlayer;

    ArenaManager arenaManager;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        arenaManager = FindObjectOfType<ArenaManager>();
        CreateCards();
        ShuffleDecks();

        StartCoroutine(InitialDraw());

        currentPlayer = Random.value < 0.5f ? "player" : "enemy";

        endTurnButton.onClick.AddListener(EndTurnButtonClicked);
    }

    IEnumerator StartTurn()
    {
        // Draw phase
        currentTurnState = TurnState.DrawPhase;
        Debug.Log("Draw Phase");
        yield return DrawPhase();

        // Main phase
        currentTurnState = TurnState.MainPhase;
        Debug.Log("Main Phase");
        yield return MainPhase();

        arenaManager.ResetEnemyAttacks();
        arenaManager.ResetPlayerAttacks();

        SwitchTurn();

        StartCoroutine(StartTurn());
    }

    void SwitchTurn()
    {
        currentPlayer = (currentPlayer == "player") ? "enemy" : "player";
    }

    IEnumerator InitialDraw()
    {
        yield return new WaitForSeconds(1f);
        DrawCardToHand(playerDeck, playerHand, "player");
        DrawCardToHand(enemyDeck, enemyHand, "enemy");
        yield return new WaitForSeconds(1f);

        DrawCardToHand(playerDeck, playerHand, "player");
        DrawCardToHand(enemyDeck, enemyHand, "enemy");
        yield return new WaitForSeconds(1f);


        DrawCardToHand(playerDeck, playerHand, "player");
        DrawCardToHand(enemyDeck, enemyHand, "enemy");

        playerCoins.coins += 3;
        enemyCoins.coins += 3;
        playerCoins.UpdateHealthText();
        enemyCoins.UpdateHealthText();


        StartCoroutine(StartTurn());
    }

    IEnumerator DrawPhase()
    {
        yield return new WaitForSeconds(1f);

        if (currentPlayer == "player")
        {
            DrawCardToHand(playerDeck, playerHand, "player");
            playerCoins.coins += 3;
            playerCoins.UpdateHealthText();

        }
        else
        {
            DrawCardToHand(enemyDeck, enemyHand, "enemy");
            enemyCoins.coins += 3;
            enemyCoins.UpdateHealthText();

        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator MainPhase()
    {
        Debug.Log($"Entering {currentPlayer}'s Main Phase");

        while (currentTurnState == TurnState.MainPhase)
        {
            yield return null;
        }
    }


    public void EndTurnButtonClicked()
    {
        if (currentTurnState == TurnState.MainPhase)
        {
            currentTurnState = TurnState.EndPhase;
        }
    }

    void ShuffleDecks()
    {
        playerDeck.ShuffleDeck();
        enemyDeck.ShuffleDeck();
    }

    public void DrawCardToHand(Deck deck, Hand hand, string owner)
    {
        Card drawnCard = deck.DrawCard();
        if (drawnCard != null)
        {
            hand.AddCard(drawnCard, owner);
        }
        else
        {
            Debug.Log("No more cards in the deck!");
        }
    }


    GameObject CreateLeader(LeaderData leader, GameObject slot)
    {
        Leader leaderData = new Leader(leader.name, 10, Resources.Load<Sprite>("Images/Cards/emiliano_zapata"));
        GameObject playerLeaderObject = Instantiate(leaderPrefab, slot.transform);
        playerLeaderObject.GetComponent<LeaderController>().SetLeaderData(leaderData);
        playerLeaderObject.GetComponent<LeaderDisplay>().LoadLeader(leaderData);
        return playerLeaderObject;
    }

    void CreateCards()
    {
        StartCoroutine(GetDeckCards("decks/2/", playerDeck, "player"));
        StartCoroutine(GetDeckCards("decks/3/", enemyDeck, "enemy"));
    }

    IEnumerator GetDeckCards(string endpoint, Deck deck, string player)
    {
        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            DeckData deckData = JsonConvert.DeserializeObject<DeckData>(data);

            foreach (CardData card in deckData.cards)
            {
                deck.AddCard(new Card(card.card_id, card.name, Resources.Load<Sprite>("Images/Cards/gladiador"), card.health, card.attack, card.cost));
                Debug.Log(card.name);
            }

            if (player == "player")
            {
                playerLeader = CreateLeader(deckData.leader, playerLeaderSlot);
            }
            else
            {
                enemyLeader = CreateLeader(deckData.leader, enemyLeaderSlot);
            }

        }


    }

    public IEnumerator SendGetRequest(string endpoint)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL + endpoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Request failed: {www.error}");

            if (!string.IsNullOrEmpty(www.downloadHandler.text))
            {
                Debug.LogError($"Error message: {www.downloadHandler.text}");
            }
        }
        else
        {
            data = www.downloadHandler.text;
        }
    }



}
