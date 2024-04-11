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
    int cost;

    ArenaManager arenaManager;

    public bool hasAttacked = false;


    void Start()
    {
        selectionHighlight.enabled = false;
        arenaManager = FindObjectOfType<ArenaManager>();
    }

    public void SetCardData(Card card)
    {
        cardData = card;
        health = cardData.health;
        cost = cardData.cost;
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
            if (arenaManager.selectedAttacker.hasAttacked)
            {
                Debug.Log("Card already attacked this turn");
            }
            else
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

                arenaManager.selectedAttacker.hasAttacked = true;
                arenaManager.selectedAttacker.DeselectCard();

            }


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

    bool CanDrag()
    {
        if (GameManager.Instance.currentPlayer == "player")
        {
            return GameManager.Instance.currentTurnState == GameManager.TurnState.MainPhase && tag == "PlayerCard";
        }
        else
        {
            return GameManager.Instance.currentTurnState == GameManager.TurnState.MainPhase && tag == "EnemyCard";
        }
    }

    bool HasEnoughCoinsToInvoke()
    {
        if (GameManager.Instance.currentPlayer == "player")
        {
            return GameManager.Instance.playerCoins.coins >= cost;
        }
        else
        {
            return GameManager.Instance.enemyCoins.coins >= cost;
        }
    }

    public bool IsInArena()
    {
        return transform.parent.GetComponent<ArenaSlot>() != null;
    }

    public bool CanInvoke()
    {
        return !IsInArena() && CanDrag() && HasEnoughCoinsToInvoke();
    }

    public void InvokeCard()
    {
        if (GameManager.Instance.currentPlayer == "player")
        {
            GameManager.Instance.playerCoins.coins -= cost;
            GameManager.Instance.playerCoins.UpdateHealthText();
        }
        else
        {
            GameManager.Instance.enemyCoins.coins -= cost;
            GameManager.Instance.enemyCoins.UpdateHealthText();
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