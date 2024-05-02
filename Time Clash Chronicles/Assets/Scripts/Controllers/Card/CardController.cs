using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject cardBack;

    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarBackground;

    [HideInInspector] public Card cardData;
    [HideInInspector] public string owner;

    [HideInInspector] public int health;
    [HideInInspector] public int cost;

    [HideInInspector] public int attack;

    [HideInInspector] public bool hasAttacked;
    [HideInInspector] public bool hasUsedAbility;

    [HideInInspector] public bool isFlipped = true;

    public Image selectionHighlight;

    [HideInInspector] public float maxHealth;

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

    // When a card is clicked on select the card or remove its selection if it is already selected
    void HandleCardSelection()
    {
        if (!arenaManager.selectedAttacker)
        {

            if (CanSelect())
            {
                // Select this card and hilight opponents card to attack
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
                // If another ally card is selected select that card and deselect this one
                if (arenaManager.selectedAttacker.tag == tag)
                {
                    arenaManager.selectedAttacker.DeselectCard();
                    SelectCard();
                    HighlightTargetCards();
                }
                else
                {
                    // Attack the clicked card
                    AttackCard(this);
                }
            }

        }
    }

    // Remove the attack hilights from the leader and opponent cards and deselect the attacker
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



    // Hilight either the opponent leader or cards to attack
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

    // Attak a card
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
                // Update cards health with the opponents attack
                int prevHealth = target.cardData.health;
                target.UpdateHealth(-arenaManager.selectedAttacker.attack);

                Debug.Log("Attack: " + arenaManager.selectedAttacker.attack + ", Target Health: " + prevHealth + " -> " + target.health);

                // If the card is dead destroy it
                if (target.health <= 0)
                {
                    CardHover cardHover = GetComponent<CardHover>();
                    cardHover.cardPreview.transform.localScale = new Vector3(0f, 0f, 0f);
                    Destroy(target.gameObject);
                }

                arenaManager.selectedAttacker.hasAttacked = true;

                // After the attack remove all card hilights and deselect the attacker
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

    // Check if the card can be selected
    bool CanSelect()
    {
        if (gameManager.currentPlayer == "player")
        {
            return tag == "PlayerCard" && transform.parent.GetComponent<ArenaSlot>() != null;
        }

        return false;
    }

    // Check if a card can be dragged into the arena
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

    // Check if a card is in the arena
    public bool IsInArena()
    {
        return transform.parent.GetComponent<ArenaSlot>() != null;
    }

    public bool CanInvoke()
    {
        return !IsInArena() && CanDrag() && HasEnoughCoinsToInvoke();
    }

    // Invoke a card and update the player coins
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

    // Select the card and activate the ability button if the card has an ability
    public void SelectCard()
    {
        arenaManager.SetSelectedAttacker(this);
        selectionHighlight.enabled = true;

        if (cardData.ability != null && !hasUsedAbility)
        {
            gameManager.activateAbilityButton.interactable = true;
        }
    }

    // Remove a card selection and deactivate the ability button if the card has an ability
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