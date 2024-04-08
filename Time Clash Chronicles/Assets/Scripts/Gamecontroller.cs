using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public enum TurnState
{
    DrawPhase,
    MainPhase,
    EndPhase
}

public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton pattern to access GameController from other scripts
    public TurnState currentTurnState;
    public Button endTurnButton;
    public CardController[] playerCards; // Array to hold player's cards

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentTurnState = TurnState.DrawPhase;
        StartCoroutine(GameLoop());
        endTurnButton.onClick.AddListener(EndTurn);
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            switch (currentTurnState)
            {
                case TurnState.DrawPhase:
                    Debug.Log("Draw Phase");
                    yield return StartCoroutine(DrawPhase());
                    currentTurnState = TurnState.MainPhase;
                    break;

                case TurnState.MainPhase:
                    Debug.Log("Main Phase");
                    yield return StartCoroutine(MainPhase());
                    currentTurnState = TurnState.EndPhase;
                    break;

                case TurnState.EndPhase:
                    Debug.Log("End Phase");
                    yield return StartCoroutine(EndPhase());
                    // EndTurn(); No lo llamo directamente, ahora se activa con el botón de finalizar turno
                    break;
            }

            yield return null;
        }
    }

    IEnumerator DrawPhase()
    {
        foreach (CardController card in playerCards)
        {
            // Simulate drawing a card
            yield return new WaitForSeconds(1f); 
            
        }
    }

    IEnumerator MainPhase()
    {
        foreach (CardController card in playerCards)
        {
            // Simulate invoking a card
            yield return new WaitForSeconds(1f); 
            

            // Simulate attacking with the card
            CardController targetCard = GetRandomOpponentCard();
            if (targetCard != null)
                card.AttackCard(targetCard);
        }
    }

    IEnumerator EndPhase()
    {
        // Perform any cleanup or preparation necessary before ending the turn
        yield return new WaitForSeconds(1f); 
    }

    void EndTurn()
    {
        
        Debug.Log("End of Turn");
        currentTurnState = TurnState.DrawPhase; 
    }

    CardController GetRandomOpponentCard()
    {
        
        if (playerCards.Length == 0)
            return null;
        
        return playerCards[Random.Range(0, playerCards.Length)];
    }
}