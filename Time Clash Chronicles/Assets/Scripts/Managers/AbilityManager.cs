using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }


    [HideInInspector] public bool isHealingAbilityActive;

    [HideInInspector] public bool isIncreaseDamageAbilityActive;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
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
        if (GameManager.Instance.currentPlayer == "player")
        {
            foreach (ArenaSlot slot in ArenaManager.Instance.playerSlots)
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
            foreach (ArenaSlot slot in ArenaManager.Instance.enemySlots)
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
