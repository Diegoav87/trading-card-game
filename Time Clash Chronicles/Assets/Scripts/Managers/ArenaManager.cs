using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ArenaManager : MonoBehaviour
{
    public List<ArenaSlot> playerSlots = new List<ArenaSlot>();
    public List<ArenaSlot> enemySlots = new List<ArenaSlot>();

    [HideInInspector] public CardController selectedAttacker;

    [SerializeField] Hand enemyHand;

    GameManager gameManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetSelectedAttacker(CardController attacker)
    {
        if (attacker != null)
        {
            selectedAttacker = attacker;
        }
        else
        {
            selectedAttacker = null;
        }

    }

    public void InvokeCardIntoArena(GameObject cardObject, CardController cardController)
    {
        ArenaSlot randomSlot = GetRandomFreeSlot(enemySlots);

        if (randomSlot != null)
        {
            cardObject.transform.SetParent(randomSlot.transform);
            cardObject.transform.localPosition = Vector3.zero;

            gameManager.enemyCoins.coins -= cardController.cost;
            gameManager.enemyCoins.UpdateCoinText();


            // cardObject.GetComponent<CardController>().FlipCard();
        }
    }

    public ArenaSlot GetRandomFreeSlot(List<ArenaSlot> slots)
    {
        List<ArenaSlot> emptySlots = slots.Where(slot => !slot.HasCard()).ToList();

        if (emptySlots.Count > 0)
        {
            int randomIndex = Random.Range(0, emptySlots.Count);
            return emptySlots[randomIndex];
        }
        else
        {
            return null;
        }
    }

    public ArenaSlot GetRandomOccupiedSlot(List<ArenaSlot> slots)
    {
        List<ArenaSlot> occupiedSlots = slots.Where(slot => slot.HasCard()).ToList();

        if (occupiedSlots.Count > 0)
        {
            int randomIndex = Random.Range(0, occupiedSlots.Count);
            return occupiedSlots[randomIndex];
        }
        else
        {
            return null;
        }
    }

    public void AttackPlayerCard()
    {
        if (!selectedAttacker.hasAttacked)
        {
            if (PlayerSlotsAreEmpty())
            {
                selectedAttacker.hasAttacked = true;
                gameManager.playerLeader.GetComponent<LeaderController>().TakeDamage(selectedAttacker.attack);
                selectedAttacker.DeselectCard();
            }
            else
            {
                ArenaSlot occupiedPlayerSlot = GetRandomOccupiedSlot(playerSlots);
                CardController playerCardController = occupiedPlayerSlot.GetComponentInChildren<CardController>();


                playerCardController.AttackCard(playerCardController);
                selectedAttacker.DeselectCard();
            }
        }

    }

    void HighlightCards(List<ArenaSlot> slots)
    {
        foreach (ArenaSlot slot in slots)
        {
            if (slot.HasCard())
            {
                CardDisplay cardUI = slot.GetComponentInChildren<CardDisplay>();

                Color color = HexToColor("#FFB1B1");
                cardUI.imageSprite.color = color;
            }
        }
    }

    void RemoveCardHighlights(List<ArenaSlot> slots)
    {
        foreach (ArenaSlot slot in slots)
        {
            if (slot.HasCard())
            {
                CardDisplay cardUI = slot.GetComponentInChildren<CardDisplay>();
                cardUI.imageSprite.color = Color.white;
            }
        }
    }

    public void HighlightPlayerCards()
    {
        HighlightCards(playerSlots);
    }

    public void HighlightEnemyCards()
    {
        HighlightCards(enemySlots);
    }

    public void RemovePlayerCardHighlights()
    {
        RemoveCardHighlights(playerSlots);
    }

    public void RemoveEnemyCardHighlights()
    {
        RemoveCardHighlights(enemySlots);
    }

    Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public bool SlotsAreEmpty(List<ArenaSlot> slots)
    {
        foreach (ArenaSlot slot in slots)
        {
            if (slot.HasCard())
            {
                return false;
            }
        }
        return true;

    }

    public bool PlayerSlotsAreEmpty()
    {
        return SlotsAreEmpty(playerSlots);
    }

    public bool EnemySlotsAreEmpty()
    {
        return SlotsAreEmpty(enemySlots);
    }

    void ResetAttacks(List<ArenaSlot> slots)
    {
        foreach (ArenaSlot slot in slots)
        {
            if (slot.HasCard())
            {
                CardController cardController = slot.GetComponentInChildren<CardController>();

                cardController.hasAttacked = false;
            }
        }
    }

    public void ResetPlayerAttacks()
    {
        ResetAttacks(playerSlots);
    }

    public void ResetEnemyAttacks()
    {
        ResetAttacks(enemySlots);
    }

}