using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image leaderImage;
    [SerializeField] Image healthBar;
    [HideInInspector] public string owner;


    float health;

    float maxHealth;

    ArenaManager arenaManager;
    APIManager apiManager;

    AudioManager audioManager;

    void Start()
    {
        arenaManager = FindObjectOfType<ArenaManager>();
        apiManager = FindObjectOfType<APIManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SetLeaderData(Leader leader)
    {
        health = leader.health;
        maxHealth = leader.health;
    }

    public void HilightLeader()
    {
        Color color = HexToColor("#FFB1B1");
        leaderImage.color = color;
    }

    public void RemoveHiglight()
    {
        leaderImage.color = Color.white;
    }

    Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    // When the leader is clicked take damage if the opponent is attacking it
    public void OnPointerClick(PointerEventData eventData)
    {
        if (arenaManager.selectedAttacker)
        {
            if (!arenaManager.selectedAttacker.hasAttacked)
            {
                if (arenaManager.selectedAttacker.tag == "PlayerCard")
                {
                    if (arenaManager.EnemySlotsAreEmpty())
                    {
                        audioManager.Play("Attack");
                        arenaManager.selectedAttacker.hasAttacked = true;
                        TakeDamage(arenaManager.selectedAttacker.attack);
                        arenaManager.selectedAttacker.DeselectCard();
                    }
                }
                else
                {
                    if (arenaManager.PlayerSlotsAreEmpty())
                    {
                        audioManager.Play("Attack");
                        arenaManager.selectedAttacker.hasAttacked = true;
                        TakeDamage(arenaManager.selectedAttacker.attack);
                        arenaManager.selectedAttacker.DeselectCard();
                    }
                }
            }
            else
            {
                Debug.Log("Card already attacked this turn");
            }
        }
    }

    // Take the damage dealt to the leader and update its health
    // If the leader dies load the defeat or victory screen
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);

            if (owner == "player")
            {
                int currentPlayer = PlayerPrefs.GetInt("CurrentPlayer");
                int selectedDeck = PlayerPrefs.GetInt("SelectedPlayerDeck");

                StartCoroutine(apiManager.CreateGame("games/", new { player_id = currentPlayer, deck_id = selectedDeck, win = false }));

                SceneManager.LoadScene("DefeatScreen");
            }
            else
            {
                int currentPlayer = PlayerPrefs.GetInt("CurrentPlayer");
                int selectedDeck = PlayerPrefs.GetInt("SelectedPlayerDeck");

                StartCoroutine(apiManager.CreateGame("games/", new { player_id = currentPlayer, deck_id = selectedDeck, win = true }));

                SceneManager.LoadScene("VictoryScreen");
            }
        }

        RemoveHiglight();
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
