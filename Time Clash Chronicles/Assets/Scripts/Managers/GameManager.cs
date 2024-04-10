using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public enum TurnState
    {
        DrawPhase,
        MainPhase,
        EndPhase
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

    [HideInInspector] public GameObject playerLeader;
    [HideInInspector] public GameObject enemyLeader;

    public TurnState currentTurnState;
    public Button endTurnButton;

    public string currentPlayer;


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
        CreateLeaders();
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

        StartCoroutine(StartTurn());
    }

    IEnumerator DrawPhase()
    {
        yield return new WaitForSeconds(1f);

        if (currentPlayer == "player")
        {
            DrawCardToHand(playerDeck, playerHand, "player");
        }
        else
        {
            DrawCardToHand(enemyDeck, enemyHand, "enemy");
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
        playerDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        playerDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        playerDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));

        enemyDeck.AddCard(new Card(1, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        enemyDeck.AddCard(new Card(2, "Caballero", "Caballero de la francia medieval", Resources.Load<Sprite>("Images/caballero_frances"), 4, 4, 4));
        enemyDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        enemyDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        enemyDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
        enemyDeck.AddCard(new Card(3, "Gladiador", "Gladiador del coliseo romano", Resources.Load<Sprite>("Images/gladiador"), 5, 6, 3));
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
