using UnityEngine;

[System.Serializable]
public class DeckData
{
    public int deck_id;
    public string name;
    public string description;
    public string nation;



    public DeckData(int deck_id, string name, string description, string nation)
    {
        this.deck_id = deck_id;
        this.name = name;
        this.description = description;
        this.nation = nation;
    }
}