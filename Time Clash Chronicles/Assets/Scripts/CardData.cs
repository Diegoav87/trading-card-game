using UnityEngine;

[System.Serializable]
public class CardData
{
    public int id;
    public string name;
    public string description;
    public int attack;
    public int health;
    public int cost;
    public bool is_leader;
    public string created_at;
    public string updated_at;

    public CardData(string name, int attack, int health, int cost, bool is_leader, string created_at, string updated_at)
    {
        this.name = name;
        this.attack = attack;
        this.health = health;
        this.cost = cost;
        this.is_leader = is_leader;
        this.created_at = created_at;
        this.updated_at = updated_at;
    }
}