using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public int id;
    public string name;

    public Sprite image;
    public int health;
    public int attack;
    public int cost;

    public CardAbility ability;

    public Sprite flag;

    public Card(int Id, string Name, Sprite Image, int Health, int Attack, int Cost, CardAbility Ability, Sprite Flag)
    {
        id = Id;
        name = Name;
        image = Image;
        health = Health;
        attack = Attack;
        cost = Cost;
        ability = Ability;
        flag = Flag;
    }
}
