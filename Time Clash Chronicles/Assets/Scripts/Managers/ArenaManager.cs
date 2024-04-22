using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ArenaManager : MonoBehaviour
{
    public static ArenaManager Instance { get; private set; }

    public List<ArenaSlot> playerSlots = new List<ArenaSlot>();
    public List<ArenaSlot> enemySlots = new List<ArenaSlot>();

    [HideInInspector] public CardController selectedAttacker;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void InvokeCardIntoArena(GameObject cardObject, Card card)
    {
        ArenaSlot randomSlot = GetRandomFreeSlot(enemySlots);

        if (randomSlot != null)
        {
            cardObject.transform.SetParent(randomSlot.transform);
            cardObject.transform.localPosition = Vector3.zero;

            GameManager.Instance.enemyCoins.coins -= card.cost;
            GameManager.Instance.enemyCoins.UpdateCoinText();
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