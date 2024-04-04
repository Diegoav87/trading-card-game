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
    DeckManager deckManager;


    void Start()
    {
        selectionHighlight.enabled = false;
        arenaManager = FindObjectOfType<ArenaManager>();
        deckManager = FindObjectOfType<DeckManager>();
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!arenaManager.selectedAttacker)
        {
            SelectCard();

            if (arenaManager.EnemySlotsAreEmpty())
            {
                deckManager.enemyLeader.GetComponent<LeaderController>().HilightLeader();
            }
        }
        else
        {
            if (arenaManager.selectedAttacker == this)
            {
                DeselectCard();
                deckManager.enemyLeader.GetComponent<LeaderController>().RemoveHiglight();

            }
            else
            {
                AttackCard(this);

            }

        }
    }

    public void AttackCard(CardController target)
    {
        ArenaSlot enemySlot = GetComponentInParent<ArenaSlot>();
        bool isTargetEnemyCard = arenaManager.enemySlots.Contains(enemySlot);

        if (arenaManager.selectedAttacker != null && target != null && isTargetEnemyCard)
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