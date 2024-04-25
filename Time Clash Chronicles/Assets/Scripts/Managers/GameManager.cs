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

    EnemyAIManager enemyAIManager;


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
        enemyAIManager = FindAnyObjectByType<EnemyAIManager>();
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

    public void HandleAbilityActivation()
    {
        if (ArenaManager.Instance.selectedAttacker != null && !ArenaManager.Instance.selectedAttacker.hasUsedAbility)
        {
            if (ArenaManager.Instance.selectedAttacker.cardData.ability != null)
            {
                AbilityManager.Instance.ActivateAbility(ArenaManager.Instance.selectedAttacker.cardData.ability, ArenaManager.Instance.selectedAttacker);
                ArenaManager.Instance.selectedAttacker.hasUsedAbility = true;
                ArenaManager.Instance.selectedAttacker.DeselectCard();
                ArenaManager.Instance.RemoveEnemyCardHighlights();
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

        if (currentPlayer == "player")
        {
            endTurnButton.interactable = true;
        }


        if (currentPlayer == "enemy")
        {
            StartCoroutine(PlayEnemyTurn());
        }
    }



    IEnumerator PlayEnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        float[] actionWeights = { 0.4f, 0.3f, 0.2f, 0.1f };

        int numActions = 2;
        // Debug.Log("Number of actions: " + numActions);

        for (int i = 0; i < numActions; i++)
        {
            int actionType = 2;
            // int actionType = WeightedRandomIndex(actionWeights);

            // Debug.Log("Selected action: " + actionType);
            // Debug.Log("Number of action: " + i);


            if (ArenaManager.Instance.EnemySlotsAreEmpty())
            {
                enemyAIManager.InvokeCard();
            }
            else
            {
                switch (actionType)
                {
                    case 0:
                        enemyAIManager.InvokeCard();
                        break;
                    case 1:
                        StartCoroutine(enemyAIManager.ActivateAbility());
                        break;
                    case 2:
                        enemyAIManager.AttackCard();
                        break;
                    case 3:
                        EndTurnButtonClicked();
                        yield break;
                    default:
                        break;

                }
            }

            yield return new WaitForSeconds(1f);

        }

        EndTurnButtonClicked();


    }

    int WeightedRandomIndex(float[] weights)
    {
        float[] cumulativeProbabilities = new float[weights.Length];
        float sum = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
            cumulativeProbabilities[i] = sum;
        }

        float randomValue = UnityEngine.Random.Range(0f, sum);


        for (int i = 0; i < cumulativeProbabilities.Length; i++)
        {
            if (randomValue < cumulativeProbabilities[i])
            {
                return i;
            }
        }

        return weights.Length - 1;
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
