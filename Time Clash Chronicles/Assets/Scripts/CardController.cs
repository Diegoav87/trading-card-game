using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IPointerClickHandler
{
    private ArenaManager arenaManager;

    public Image selectionHighlight;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public Card cardData;

    public int health;


    void Start()
    {
        selectionHighlight.enabled = false;
        arenaManager = FindObjectOfType<ArenaManager>();
        InitializeCard();
    }

    private void InitializeCard()
    {
        health = cardData.health;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!arenaManager.selectedAttacker)
        {
            SelectCard();
        }
        else
        {
            Attack(this);
        }
    }

    public void Attack(CardController target)
    {
        bool isTargetEnemyCard = arenaManager.enemyCards.Contains(target);

        if (arenaManager.selectedAttacker != null && target != null && isTargetEnemyCard)
        {
            if (arenaManager.selectedAttacker.cardData != null)
            {
                int prevHealth = target.cardData.health;
                int currentHealth = prevHealth - arenaManager.selectedAttacker.cardData.attack;

                Debug.Log("Attack: " + arenaManager.selectedAttacker.cardData.attack + ", Target Health: " + prevHealth + " -> " + currentHealth);

                target.health = currentHealth;
                target.UpdateHealthText();

                if (currentHealth <= 0)
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
