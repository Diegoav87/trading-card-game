using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image selectionHighlight;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI healthText;

    [HideInInspector] public Card cardData;
    [HideInInspector] public string owner;

    int health;

    ArenaManager arenaManager;


    void Start()
    {
        selectionHighlight.enabled = false;
        arenaManager = FindObjectOfType<ArenaManager>();
        InitializeCard();
    }

    public void SetCardData(Card card)
    {
        cardData = card;
        InitializeCard();
    }

    private void InitializeCard()
    {
        health = cardData.health;
        string cardTag = owner == "player" ? "PlayerCard" : "EnemyCard";
        gameObject.tag = cardTag;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.currentTurnState == GameManager.TurnState.MainPhase)
        {
            if (!arenaManager.selectedAttacker)
            {

                if (CanSelect())
                {
                    SelectCard();

                    if (arenaManager.selectedAttacker.tag == "PlayerCard")
                    {
                        if (arenaManager.EnemySlotsAreEmpty())
                        {
                            GameManager.Instance.enemyLeader.GetComponent<LeaderController>().HilightLeader();
                        }
                    }
                    else
                    {
                        if (arenaManager.PlayerSlotsAreEmpty())
                        {
                            GameManager.Instance.playerLeader.GetComponent<LeaderController>().HilightLeader();
                        }
                    }
                }




            }
            else
            {
                if (arenaManager.selectedAttacker == this)
                {

                    if (tag == "PlayerCard")
                    {
                        GameManager.Instance.enemyLeader.GetComponent<LeaderController>().RemoveHiglight();
                    }
                    else
                    {
                        GameManager.Instance.playerLeader.GetComponent<LeaderController>().RemoveHiglight();
                    }

                    DeselectCard();


                }
                else
                {
                    AttackCard(this);

                }

            }
        }


    }

    public void AttackCard(CardController target)
    {
        if (arenaManager.selectedAttacker != null && target != null)
        {
            if (arenaManager.selectedAttacker.cardData != null)
            {
                int prevHealth = target.cardData.health;
                health = health - arenaManager.selectedAttacker.cardData.attack;

                Debug.Log("Attack: " + arenaManager.selectedAttacker.cardData.attack + ", Target Health: " + prevHealth + " -> " + health);

                target.health = health;
                target.UpdateHealthText();

                if (health <= 0)
                {
                    Destroy(target.gameObject);
                }
            }
            else
            {
                Debug.Log("Selected attacker has no card data.");
            }

            arenaManager.selectedAttacker.DeselectCard();
        }
        else
        {
            Debug.Log("Invalid attack.");
        }
    }

    bool CanSelect()
    {
        if (GameManager.Instance.currentPlayer == "player")
        {
            return tag == "PlayerCard" && transform.parent.GetComponent<ArenaSlot>() != null;
        }
        else
        {
            return tag == "EnemyCard" && transform.parent.GetComponent<ArenaSlot>() != null;
        }
    }

    void SelectCard()
    {
        arenaManager.SetSelectedAttacker(this);
        selectionHighlight.enabled = true;
    }

    public void DeselectCard()
    {
        arenaManager.SetSelectedAttacker(null);
        selectionHighlight.enabled = false;
    }

    public void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    void ResetHealth()
    {
        UpdateHealthText();
    }

    void OnDisable()
    {
        ResetHealth();
    }
}