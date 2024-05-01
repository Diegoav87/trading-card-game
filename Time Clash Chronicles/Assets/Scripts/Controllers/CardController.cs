using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    public Image selectionHighlight;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject cardBack;

    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarBackground;

    [HideInInspector] public Card cardData;
    [HideInInspector] public string owner;

    float maxHealth;

    public int health;
    public int cost;

    public int attack;



    public bool hasAttacked;
    public bool hasUsedAbility;

    public bool isFlipped = true;

    ArenaManager arenaManager;
    GameManager gameManager;

    AudioManager audioManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        arenaManager = FindObjectOfType<ArenaManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        healthBar.enabled = false;
        healthBarBackground.enabled = false;
        selectionHighlight.enabled = false;
        hasAttacked = false;
        hasUsedAbility = false;
    }

    public void SetCardData(Card card)
    {
        cardData = card;
        health = cardData.health;
        maxHealth = cardData.health;
        cost = cardData.cost;
        attack = cardData.attack;
        string cardTag = owner == "player" ? "PlayerCard" : "EnemyCard";
        gameObject.tag = cardTag;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager.currentTurnState == GameManager.TurnState.MainPhase && !gameManager.IsFirstTurn())
        {
            HandleCardSelection();
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
                    arenaManager.selectedAttacker.DeselectCard();
                    SelectCard();
                    HighlightTargetCards();
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
            gameManager.enemyLeader.GetComponent<LeaderController>().RemoveHiglight();
            arenaManager.RemoveEnemyCardHighlights();
        }
        else
        {
            gameManager.playerLeader.GetComponent<LeaderController>().RemoveHiglight();
            arenaManager.RemovePlayerCardHighlights();
        }

        DeselectCard();
    }



    private void HighlightTargetCards()
    {
        if (tag == "PlayerCard")
        {
            if (arenaManager.EnemySlotsAreEmpty())
                gameManager.enemyLeader.GetComponent<LeaderController>().HilightLeader();
            else
                arenaManager.HighlightEnemyCards();
        }
        else
        {
            if (arenaManager.PlayerSlotsAreEmpty())
                gameManager.playerLeader.GetComponent<LeaderController>().HilightLeader();
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

                Debug.Log("Attack: " + arenaManager.selectedAttacker.attack + ", Target Health: " + prevHealth + " -> " + target.health);

                if (target.health <= 0)
                {
                    CardHover cardHover = GetComponent<CardHover>();
                    cardHover.cardPreview.transform.localScale = new Vector3(0f, 0f, 0f);
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

                audioManager.Play("Attack");

            }


        }
        else
        {
            Debug.Log("Invalid attack.");
        }
    }

    bool CanSelect()
    {
        if (gameManager.currentPlayer == "player")
        {
            return tag == "PlayerCard" && transform.parent.GetComponent<ArenaSlot>() != null;
        }

        return false;
    }

    bool CanDrag()
    {
        if (gameManager.currentPlayer == "player")
        {
            return gameManager.currentTurnState == GameManager.TurnState.MainPhase && tag == "PlayerCard";
        }

        return false;
    }

    bool HasEnoughCoinsToInvoke()
    {
        if (gameManager.currentPlayer == "player")
        {
            return gameManager.playerCoins.coins >= cost;
        }
        else
        {
            return gameManager.enemyCoins.coins >= cost;
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
        if (gameManager.currentPlayer == "player")
        {
            gameManager.playerCoins.coins -= cost;
            gameManager.playerCoins.UpdateCoinText();
            audioManager.Play("Invoke");
        }
        else
        {
            gameManager.enemyCoins.coins -= cost;
            gameManager.enemyCoins.UpdateCoinText();
        }
    }

    public void SelectCard()
    {
        arenaManager.SetSelectedAttacker(this);
        selectionHighlight.enabled = true;

        if (cardData.ability != null && !hasUsedAbility)
        {
            gameManager.activateAbilityButton.interactable = true;
        }
    }


    public void DeselectCard()
    {
        arenaManager.SetSelectedAttacker(null);
        selectionHighlight.enabled = false;

        if (cardData.ability != null)
        {
            gameManager.activateAbilityButton.interactable = false;
        }
    }

    public void UpdateHealth(int amount)
    {
        health += amount;
        UpdateHealthText();
        UpdateHealthBar();
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

    public void FlipCard()
    {
        isFlipped = !isFlipped;
        cardBack.SetActive(isFlipped);
    }

    void UpdateHealthBar()
    {
        healthBar.enabled = true;
        healthBarBackground.enabled = true;
        healthBar.fillAmount = health / maxHealth;
    }
}