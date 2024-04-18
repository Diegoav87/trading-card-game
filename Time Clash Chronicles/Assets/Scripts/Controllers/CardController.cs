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

    int attack;

    ArenaManager arenaManager;

    public bool hasAttacked;
    public bool hasUsedAbility;


    void Start()
    {
        selectionHighlight.enabled = false;
        arenaManager = FindObjectOfType<ArenaManager>();
        hasAttacked = false;
        hasUsedAbility = false;
    }

    public void SetCardData(Card card)
    {
        cardData = card;
        health = cardData.health;
        cost = cardData.cost;
        attack = cardData.attack;
        string cardTag = owner == "player" ? "PlayerCard" : "EnemyCard";
        gameObject.tag = cardTag;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.currentTurnState == GameManager.TurnState.MainPhase)
        {
            if (AbilityManager.Instance.isHealingAbilityActive)
            {
                HandleHealingAbility();
            }
            else
            {

                if (AbilityManager.Instance.isIncreaseDamageAbilityActive)
                {
                    HandleIncreaseDamageAbility();
                }
                else
                {
                    HandleCardSelection();

                }

            }


        }


    }

    void HandleCardSelection()
    {
        if (!arenaManager.selectedAttacker)
        {

            if (CanSelect())
            {
                SelectCard();
                HighlightTargetCards();
            }
        }
        else
        {
            if (arenaManager.selectedAttacker == this)
            {
                RemoveHighlightsAndDeselectCard();
            }
            else
            {
                if (arenaManager.selectedAttacker.tag == tag)
                {
                    Debug.Log("You can't attack allies");
                }
                else
                {
                    AttackCard(this);

                }


            }

        }
    }

    void RemoveHighlightsAndDeselectCard()
    {
        if (tag == "PlayerCard")
        {
            GameManager.Instance.enemyLeader.GetComponent<LeaderController>().RemoveHiglight();
            arenaManager.RemoveEnemyCardHighlights();
        }
        else
        {
            GameManager.Instance.playerLeader.GetComponent<LeaderController>().RemoveHiglight();
            arenaManager.RemovePlayerCardHighlights();
        }

        DeselectCard();
    }

    void HandleHealingAbility()
    {
        if ((tag == "PlayerCard" && GameManager.Instance.currentPlayer == "player") ||
            (tag == "EnemyCard" && GameManager.Instance.currentPlayer == "enemy"))
        {
            UpdateHealth(3);
            arenaManager.RemoveEnemyCardHighlights();
            arenaManager.RemovePlayerCardHighlights();
            arenaManager.selectedAttacker.DeselectCard();
            AbilityManager.Instance.isHealingAbilityActive = false;
        }
    }

    void HandleIncreaseDamageAbility()
    {
        if ((tag == "PlayerCard" && GameManager.Instance.currentPlayer == "player") ||
            (tag == "EnemyCard" && GameManager.Instance.currentPlayer == "enemy"))
        {
            UpdateAttack(3);
            arenaManager.RemoveEnemyCardHighlights();
            arenaManager.RemovePlayerCardHighlights();
            arenaManager.selectedAttacker.DeselectCard();
            AbilityManager.Instance.isIncreaseDamageAbilityActive = false;
        }
    }


    private void HighlightTargetCards()
    {
        if (tag == "PlayerCard")
        {
            if (arenaManager.EnemySlotsAreEmpty())
                GameManager.Instance.enemyLeader.GetComponent<LeaderController>().HilightLeader();
            else
                arenaManager.HighlightEnemyCards();
        }
        else
        {
            if (arenaManager.PlayerSlotsAreEmpty())
                GameManager.Instance.playerLeader.GetComponent<LeaderController>().HilightLeader();
            else
                arenaManager.HighlightPlayerCards();
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
                target.UpdateHealth(-arenaManager.selectedAttacker.attack);

                Debug.Log("Attack: " + arenaManager.selectedAttacker.attack + ", Target Health: " + prevHealth + " -> " + health);

                if (health <= 0)
                {
                    Destroy(target.gameObject);
                }

                arenaManager.selectedAttacker.hasAttacked = true;

                if (arenaManager.selectedAttacker.tag == "PlayerCard")
                {
                    arenaManager.RemoveEnemyCardHighlights();
                }
                else
                {
                    arenaManager.RemovePlayerCardHighlights();
                }

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

        if (cardData.ability != null && !hasUsedAbility)
        {
            GameManager.Instance.activateAbilityButton.interactable = true;
        }
    }

    public void DeselectCard()
    {
        arenaManager.SetSelectedAttacker(null);
        selectionHighlight.enabled = false;

        if (cardData.ability != null)
        {
            GameManager.Instance.activateAbilityButton.interactable = false;
        }
    }

    public void UpdateHealth(int amount)
    {
        health += amount;
        UpdateHealthText();
    }

    public void UpdateAttack(int amount)
    {
        attack += amount;
        UpdateAttackText();
    }

    public void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    public void UpdateAttackText()
    {
        if (attackText != null)
        {
            attackText.text = attack.ToString();
        }
    }
}