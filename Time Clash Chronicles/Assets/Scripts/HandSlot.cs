using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    void SetCard(GameObject card)
    {
        card.transform.SetParent(transform);
    }
}
