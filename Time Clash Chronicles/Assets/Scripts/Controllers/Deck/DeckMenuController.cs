using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class DeckMenuController : MonoBehaviour
{
    [SerializeField] GameObject leaderPrefab;
    [SerializeField] GameObject leaderGrid;

    [SerializeField] Button goBackButton;

    [SerializeField] Button selectButton;
    [HideInInspector] public int currentDeckId;


    public GameObject leaderSlot;

    public GameObject cardGrid;

    public GameObject leaderCanvas;

    public GameObject cardCanvas;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Add event listeners to buttons, turn on the canvas, and get all the deck data to load the leaders
    void Start()
    {
        audioManager.Play("Menu");

        selectButton.onClick.AddListener(SelectDeck);
        goBackButton.onClick.AddListener(GoBack);

        cardCanvas.SetActive(false);

        StartCoroutine(APIManager.Instance.GetDecks("decks/", (decks) =>
        {
            foreach (DeckData deck in decks)
            {
                GameObject leader = Instantiate(leaderPrefab, leaderGrid.transform);
                leader.GetComponent<LeaderViewDisplay>().LoadLeader(deck.leader.leader_id, deck.leader.name);
                leader.GetComponent<LeaderViewController>().SetData(deck.deck_id);
            }
        }));
    }

    // Go back to the leader select screen
    void GoBack()
    {
        RemoveAllChildren(cardGrid.transform);
        RemoveAllChildren(leaderSlot.transform);
        cardCanvas.SetActive(false);
        leaderCanvas.SetActive(true);
    }

    // Select a deck and load the game scene
    void SelectDeck()
    {
        PlayerPrefs.SetInt("SelectedPlayerDeck", currentDeckId);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void RemoveAllChildren(Transform parent)
    {

        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}
