using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;

using System;
using System.Buffers;


public class GameManager : MonoBehaviour
{
    public enum TurnState
    {
        DrawPhase,
        MainPhase,
    }
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

    [HideInInspector] public string currentPlayer;

    [HideInInspector] public TurnState currentTurnState;

    public CoinController playerCoins;

    public CoinController enemyCoins;

    public Button endTurnButton;

    public Button activateAbilityButton;

    ArenaManager arenaManager;

    EnemyAIManager enemyAIManager;
    AbilityManager abilityManager;

    AudioManager audioManager;

    int currentTurn;


    void Awake()
    {
        arenaManager = FindObjectOfType<ArenaManager>();
        enemyAIManager = FindAnyObjectByType<EnemyAIManager>();
        abilityManager = FindAnyObjectByType<AbilityManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Add listeners to buttons and disable them
    // Start the call to create the decks
    void Start()
    {
        audioManager.Play("GameTheme");
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

    // If the selected attacker has not used its ability activate it
    public void HandleAbilityActivation()
    {
        if (arenaManager.selectedAttacker != null && !arenaManager.selectedAttacker.hasUsedAbility)
        {
            if (arenaManager.selectedAttacker.cardData.ability != null)
            {
                abilityManager.ActivateAbility(arenaManager.selectedAttacker.cardData.ability, arenaManager.selectedAttacker);
                arenaManager.selectedAttacker.hasUsedAbility = true;
                arenaManager.selectedAttacker.DeselectCard();
                arenaManager.RemoveEnemyCardHighlights();
                audioManager.Play("Ability");
            }

        }
    }

    // Insantiate a leader object and load its data
    GameObject CreateLeader(LeaderData leader, GameObject slot, string owner)
    {
        Leader leaderData = new Leader(leader.name, 10, Resources.Load<Sprite>("Images/Leaders/" + leader.leader_id), Resources.Load<Sprite>("Images/Leaders/Flags/" + leader.leader_id), Resources.Load<Sprite>("Images/Leaders/Borders/" + leader.leader_id));
        GameObject playerLeaderObject = Instantiate(leaderPrefab, slot.transform);
        playerLeaderObject.GetComponent<LeaderController>().SetLeaderData(leaderData);
        playerLeaderObject.GetComponent<LeaderController>().owner = owner;
        playerLeaderObject.GetComponent<LeaderDisplay>().LoadLeader(leaderData);
        return playerLeaderObject;
    }

    // Create a deck with its cards and the leader
    void CreateDeck(DeckData deckData, string player, Deck deck)
    {
        foreach (CardData card in deckData.cards)
        {
            CardAbility ability = null;

            // If the card has an ability set the class of the corresponding ability
            if (card.ability != null)
            {
                if (card.ability.ability_id == 1)
                {
                    ability = new AttackLeader(this, abilityManager);
                }
                else if (card.ability.ability_id == 2)
                {
                    ability = new ReduceAllDamage(this, arenaManager, abilityManager);
                }
                else if (card.ability.ability_id == 3)
                {
                    ability = new IncreaseAllDamage(this, arenaManager, abilityManager);
                }
                else if (card.ability.ability_id == 4)
                {
                    ability = new StealAllLife(this, arenaManager, abilityManager);
                }
                else if (card.ability.ability_id == 5)
                {
                    ability = new HealAll(this, arenaManager, abilityManager);
                }
            }

            // Add the card to the deck list
            deck.AddCard(new Card(card.card_id, card.name, Resources.Load<Sprite>("Images/CardsImages/" + card.card_id), card.health, card.attack, card.cost, ability, Resources.Load<Sprite>("Images/Leaders/Flags/" + deckData.leader.leader_id), card.ability));
        }

        // Create the leader object
        if (player == "player")
        {
            playerLeader = CreateLeader(deckData.leader, playerLeaderSlot, "player");
        }
        else
        {
            enemyLeader = CreateLeader(deckData.leader, enemyLeaderSlot, "enemy");
        }
    }

    // Get a random deck from the decks that are not being used
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

    // Start the deck creations and shuffle them
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

        // Start the initial draw phase
        StartCoroutine(InitialDraw());
    }


    void SwitchTurn()
    {
        currentPlayer = (currentPlayer == "player") ? "enemy" : "player";
    }

    // Draw 3 cards and give 3 coins to each player
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

    // Draw a card and give 3 coins to the current player
    IEnumerator DrawPhase()
    {
        currentTurn += 1;

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

        // If the current turn is of the player activate the end turn button
        if (currentPlayer == "player")
        {
            endTurnButton.interactable = true;
        }


        // If the enemy ai is playing activate its actions
        if (currentPlayer == "enemy")
        {
            StartCoroutine(PlayEnemyTurn());
        }
    }

    public bool IsFirstTurn()
    {
        return currentTurn <= 2;
    }

    // Activate the enemy AI
    IEnumerator PlayEnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        // The probabilities for the ai to perform certain action
        float[] actionWeights = { 0.2f, 0.4f, 0.4f };

        // Total number of actions the ai can make in one turn
        int numActions = 5;

        for (int i = 0; i < numActions; i++)
        {
            // Get the a random action for the ai to perform based on the probabilities
            int actionType = WeightedRandomIndex(actionWeights);

            // If it is the first turn or the ai slots are empty prioritize invoking cards
            if (arenaManager.EnemySlotsAreEmpty() || IsFirstTurn())
            {
                enemyAIManager.InvokeCard();
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
                        StartCoroutine(enemyAIManager.AttackCard());
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

    // Based on the weights pased to the function get a random value
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


    // End the current turn and reset the cards attacks
    public void EndTurnButtonClicked()
    {
        SwitchTurn();
        arenaManager.ResetEnemyAttacks();
        arenaManager.ResetPlayerAttacks();
        currentTurnState = TurnState.DrawPhase;
        endTurnButton.interactable = false;
        audioManager.Play("ChangeTurn");
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
