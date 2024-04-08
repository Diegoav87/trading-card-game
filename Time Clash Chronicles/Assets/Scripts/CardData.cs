using UnityEngine;

[System.Serializable]
public class CardData
{
    public int card_id;
    public string name;
    public int attack;
    public int health;
    public string basic_attack;
    public int cost;
    public string nation;

    public int leader_id;
    public int? ability_id;
    public int deck_id;

    public CardData(int card_id, string name, int attack, int health, string basic_attack, int cost, string nation, int leader_id, int? ability_id, int deck_id)
    {
        this.card_id = card_id;
        this.name = name;
        this.attack = attack;
        this.health = health;
        this.basic_attack = basic_attack;
        this.cost = cost;
        this.nation = nation;
        this.leader_id = leader_id;
        this.ability_id = ability_id;
        this.deck_id = deck_id;
    }
}
