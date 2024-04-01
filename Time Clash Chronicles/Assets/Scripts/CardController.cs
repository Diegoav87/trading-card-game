using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    private bool isSelected = false;
    private CardController selectedCardController;
    private ArenaManager arenaManager;

    public Image selectionHighlight;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public Card cardData;

    private int initialHealth; // Store initial health value

    
void Start()
{
    selectionHighlight.enabled = false;
    arenaManager = FindObjectOfType<ArenaManager>();

    if (cardData != null)
    {
        initialHealth = cardData.health;
        ResetHealth();
    }
    else
    {
        Debug.LogError("Card data is not assigned to CardController: " + gameObject.name);
    }
}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            SelectCard();
        }
        else
        {
            Attack();
        }
    }

    void SelectCard()
    {
        isSelected = true;
        selectedCardController = this;
        selectionHighlight.enabled = true;

        if (attackText != null && healthText != null)
        {
            attackText.text = "Attack: " + cardData.attack.ToString();
            healthText.text = "Health: " + cardData.health.ToString();
        }
    }

    void Attack()
    {
        // Check if the selected card belongs to the enemy's side
        bool isEnemyCard = arenaManager.enemyCards.Contains(selectedCardController);

        if (isEnemyCard)
        {
            int prevHealth = selectedCardController.cardData.health;
            int currentHealth = prevHealth - cardData.attack;

            Debug.Log("Attack: " + cardData.attack + ", Enemy Health: " + prevHealth + " -> " + currentHealth);

            selectedCardController.cardData.health = currentHealth;
            selectedCardController.UpdateHealthText();

            if (currentHealth <= 0)
            {
                Destroy(selectedCardController.gameObject); // Destroy the card if its health reaches 0
            }
        }
        else
        {
            Debug.Log("Please select an enemy card to attack.");
        }

        DeselectCard();
    }

    void DeselectCard()
    {
        isSelected = false;
        selectedCardController = null;
        selectionHighlight.enabled = false;

        if (attackText != null && healthText != null)
        {
            attackText.text = "";
            healthText.text = "";
        }
    }

    public void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + cardData.health.ToString();
        }
    }

    void ResetHealth()
    {
        cardData.health = initialHealth; // Reset card's health to its initial value
        UpdateHealthText();
    }

    void OnDisable()
    {
        // Reset health when the scene is stopped in the Unity Editor
        ResetHealth();
    }
}
