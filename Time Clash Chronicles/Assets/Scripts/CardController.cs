using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // Add this line to include TextMeshPro namespace

public class CardController : MonoBehaviour, IPointerClickHandler
{
    private bool isSelected = false;
    public Image selectionHighlight;
    private CardController selectedCardController;
    private ArenaManager arenaManager; // Reference to the ArenaManager

    // Add a field to reference the TextMeshPro Text component
    public TextMeshProUGUI healthText;

    // Add a field to reference the Card scriptable object
    public Card cardData;

    void Start()
    {
        selectionHighlight.enabled = false;
        arenaManager = FindObjectOfType<ArenaManager>(); // Find the ArenaManager in the scene
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelection();

        if (isSelected)
        {
            if (selectedCardController != null)
            {
                // Check if the selected card is in the enemy player's field
                if (arenaManager.enemyCards.Contains(selectedCardController))
                {
                    // Attack the selected card
                    Debug.Log(gameObject.name + " attacks " + selectedCardController.gameObject.name);

                    // Reduce the health of the attacked card
                    selectedCardController.TakeDamage(cardData.attack);

                    // Update health text
                    UpdateHealthText();
                }
                else
                {
                    Debug.Log("Cannot attack this card.");
                }
            }
            else
            {
                Debug.Log("No card selected to attack.");
            }
        }
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        selectionHighlight.enabled = isSelected;

        // Set the selected card controller if the card is selected
        if (isSelected)
        {
            // Set the selected card controller from the clicked card
            selectedCardController = this;
        }
        else
        {
            // Clear the selected card controller if the card is deselected
            selectedCardController = null;
        }
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        cardData.health -= damage;
        Debug.Log(gameObject.name + " takes " + damage + " damage. Remaining health: " + cardData.health);

        // Check if the card has been defeated
        if (cardData.health <= 0)
        {
            Debug.Log(gameObject.name + " has been defeated!");
            Destroy(gameObject);
        }

        // Update health text
        UpdateHealthText();
    }

    // Method to update the health text
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + cardData.health.ToString();
        }
    }
}