using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [HideInInspector] public bool isHealingAbilityActive;

    [HideInInspector] public bool isIncreaseDamageAbilityActive;

    GameManager gameManager;
    ArenaManager arenaManager;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        arenaManager = FindAnyObjectByType<ArenaManager>();
    }

    public void ActivateAbility(CardAbility ability, CardController cardController)
    {
        if (ability != null)
        {

            ability.Execute(cardController);
        }
    }

    public void HighlightAllies()
    {
        if (gameManager.currentPlayer == "player")
        {
            foreach (ArenaSlot slot in arenaManager.playerSlots)
            {
                if (slot.HasCard())
                {
                    CardDisplay cardUI = slot.GetComponentInChildren<CardDisplay>();
                    cardUI.imageSprite.color = Color.green;
                }
            }
        }
        else
        {
            foreach (ArenaSlot slot in arenaManager.enemySlots)
            {
                if (slot.HasCard())
                {
                    CardDisplay cardUI = slot.GetComponentInChildren<CardDisplay>();
                    cardUI.imageSprite.color = Color.green;
                }
            }
        }
    }



}
