using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;

using System;


public class GameManager : MonoBehaviour
{
    public enum TurnState
    {
        DrawPhase,
        MainPhase,
    }

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

    GameObject CreateLeader(LeaderData leader, GameObject slot)
    {
        Leader leaderData = new Leader(leader.name, 10, Resources.Load<Sprite>("Images/Leaders/" + leader.leader_id), Resources.Load<Sprite>("Images/Leaders/Flags/" + leader.leader_id), Resources.Load<Sprite>("Images/Leaders/Borders/" + leader.leader_id));
        GameObject playerLeaderObject = Instantiate(leaderPrefab, slot.transform);
        playerLeaderObject.GetComponent<LeaderController>().SetLeaderData(leaderData);
        playerLeaderObject.GetComponent<LeaderDisplay>().LoadLeader(leaderData);
        return playerLeaderObject;
    }

    void CreateDeck(DeckData deckData, string player, Deck deck)
    {
        foreach (CardData card in deckData.cards)
        {
            deck.AddCard(new Card(card.card_id, card.name, Resources.Load<Sprite>("Images/CardsImages/" + card.card_id), card.health, card.attack, card.cost, new IncreaseAllDamage(), Resources.Load<Sprite>("Images/Leaders/Flags/" + deckData.leader.leader_id)));

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

    int GenerateRandomDeckId(int usedId)
    {
        List<int> availableDecks = new List<int>();

        for (int i = 1; i <= 5; i++)
        {
            if (i != usedId)
            {
                availableDecks.Add(i);
            }
        }

        int randomIndex = UnityEngine.Random.Range(0, availableDecks.Count);
        int randomDeckNumber = availableDecks[randomIndex];
        return randomDeckNumber;
    }

    IEnumerator CreateAndShuffleDecks()
    {
        int deckId = PlayerPrefs.GetInt("SelectedPlayerDeck");

        yield return StartCoroutine(APIManager.Instance.GetDeck("decks/" + deckId + "/", (deckData) =>
        {
            CreateDeck(deckData, "player", playerDeck);
        }));

        int randomDeckNumber = GenerateRandomDeckId(deckId);

        yield return StartCoroutine(APIManager.Instance.GetDeck("decks/" + randomDeckNumber + "/", (deckData) =>
        {
            CreateDeck(deckData, "enemy", enemyDeck);
        }));


        ShuffleDecks();

        currentPlayer = UnityEngine.Random.value < 0.5f ? "player" : "enemy";

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
        playerCoins.UpdateCoinText();
        enemyCoins.UpdateCoinText();

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
            playerCoins.UpdateCoinText();

        }
        else
        {
            DrawCardToHand(enemyDeck, enemyHand, "enemy");
            enemyCoins.coins += 3;
            enemyCoins.UpdateCoinText();

        }

        yield return new WaitForSeconds(1f);

        currentTurnState = TurnState.MainPhase;
        endTurnButton.interactable = true;

        if (currentPlayer == "enemy")
        {
            StartCoroutine(PlayEnemyTurn());
        }
    }

    IEnumerator PlayEnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        Tuple<Card, GameObject> selectedCardTuple = enemyHand.SelectCardFromEnemyHand();

        if (selectedCardTuple != null)
        {
            Card selectedCard = selectedCardTuple.Item1;
            GameObject selectedCardObject = selectedCardTuple.Item2;

            arenaManager.InvokeCardIntoArena(selectedCardObject, selectedCard);

            yield return new WaitForSeconds(1f);
        }
        else
        {
            EndTurnButtonClicked();
        }
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


}
