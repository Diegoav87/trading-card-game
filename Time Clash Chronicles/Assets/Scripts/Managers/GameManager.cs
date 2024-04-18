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

    public Button activateAbilityButton;

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
        activateAbilityButton.onClick.AddListener(ActivateAbilityButtonClicked);
        endTurnButton.onClick.AddListener(EndTurnButtonClicked);

        endTurnButton.interactable = false;
        activateAbilityButton.interactable = false;

        StartCoroutine(CreateAndShuffleDecks());
    }

    void ActivateAbilityButtonClicked()
    {
        HandleAbilityActivation();
    }

    private void HandleAbilityActivation()
    {
        if (ArenaManager.Instance.selectedAttacker != null && !ArenaManager.Instance.selectedAttacker.hasUsedAbility)
        {
            if (ArenaManager.Instance.selectedAttacker.cardData.ability != null)
            {
                AbilityManager.Instance.ActivateAbility(ArenaManager.Instance.selectedAttacker.cardData.ability, ArenaManager.Instance.selectedAttacker);
                ArenaManager.Instance.selectedAttacker.hasUsedAbility = true;
            }
            else
            {
                Debug.LogWarning("No ability assigned to this card.");
            }
        }
    }

    IEnumerator CreateAndShuffleDecks()
    {
        yield return StartCoroutine(GetDeckCards("decks/2/", playerDeck, "player"));
        yield return StartCoroutine(GetDeckCards("decks/3/", enemyDeck, "enemy"));

        ShuffleDecks();

        currentPlayer = Random.value < 0.5f ? "player" : "enemy";

        StartCoroutine(InitialDraw());
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

        currentTurnState = TurnState.DrawPhase;
        StartCoroutine(DrawPhase());
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

        currentTurnState = TurnState.MainPhase;
        endTurnButton.interactable = true;
    }


    public void EndTurnButtonClicked()
    {
        SwitchTurn();
        arenaManager.ResetEnemyAttacks();
        arenaManager.ResetPlayerAttacks();
        currentTurnState = TurnState.DrawPhase;
        endTurnButton.interactable = false;
        StartCoroutine(DrawPhase());
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



    IEnumerator GetDeckCards(string endpoint, Deck deck, string player)
    {
        yield return StartCoroutine(SendGetRequest(endpoint));

        if (data != null)
        {
            DeckData deckData = JsonConvert.DeserializeObject<DeckData>(data);

            foreach (CardData card in deckData.cards)
            {
                deck.AddCard(new Card(card.card_id, card.name, Resources.Load<Sprite>("Images/Cards/gladiador"), card.health, card.attack, card.cost, new IncreaseAllDamage()));
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
