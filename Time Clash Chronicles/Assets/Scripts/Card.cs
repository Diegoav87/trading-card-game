using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public int id;
    public string name;
    public string description;
    public Sprite image;
    public int health;
    public int attack;
    public int cost;

    public Card(int Id, string Name, string Description, Sprite Image, int Health, int Attack, int Cost)
    {
        id = Id;
        name = Name;
        description = Description;
        image = Image;
        health = Health;
        attack = Attack;
        cost = Cost;
    }
}
