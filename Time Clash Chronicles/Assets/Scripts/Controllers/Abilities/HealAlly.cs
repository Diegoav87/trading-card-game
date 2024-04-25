using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAlly : CardAbility
{
    public void Execute(CardController cardController)
    {
        AbilityManager.Instance.isHealingAbilityActive = true;
        AbilityManager.Instance.HighlightAllies();
    }


}
