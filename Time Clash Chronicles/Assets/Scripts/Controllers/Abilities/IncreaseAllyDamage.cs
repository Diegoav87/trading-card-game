using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAllyDamage : CardAbility
{
    public void Execute(CardController cardController)
    {
        AbilityManager.Instance.isIncreaseDamageAbilityActive = true;
        AbilityManager.Instance.HighlightAllies();
    }
}
