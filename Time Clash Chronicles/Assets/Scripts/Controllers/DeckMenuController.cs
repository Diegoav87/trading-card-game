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

    public GameObject leaderSlot;

    public GameObject cardGrid;

    public GameObject leaderCanvas;

    public GameObject cardCanvas;

    [SerializeField] Button goBackButton;

    [SerializeField] Button selectButton;

    public int currentDeckId;

    void Start()
    {
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

    void GoBack()
    {
        RemoveAllChildren(cardGrid.transform);
        RemoveAllChildren(leaderSlot.transform);
        cardCanvas.SetActive(false);
        leaderCanvas.SetActive(true);
    }

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
