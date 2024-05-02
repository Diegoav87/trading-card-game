using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeader : CardAbility
{
    GameManager gameManager;
    AbilityManager abilityManager;

    public AttackLeader(GameManager gManager, AbilityManager abManager)
    {
        gameManager = gManager;
        abilityManager = abManager;
    }

    // Attack the opponent leader directly skipping the cards
    public void Execute(CardController cardController)
    {
        if (gameManager.currentPlayer == "player")
        {
            LeaderController leaderController = gameManager.enemyLeader.GetComponent<LeaderController>();
            int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);
            leaderController.TakeDamage(value);
        }
        else
        {
            LeaderController leaderController = gameManager.playerLeader.GetComponent<LeaderController>();
            int value = abilityManager.GetAbilityValue(cardController.cardData.abilityData.ability_id, cardController.cardData.id);
            leaderController.TakeDamage(value);
        }
    }
}