using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckData
{
    public int deck_id;
    public string name;
    public string description;
    public string nation;

    public LeaderData leader;
    public List<CardData> cards;

}