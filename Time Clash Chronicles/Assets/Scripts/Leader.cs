using UnityEngine;

[System.Serializable]
public class Leader
{
    public string name;
    public string description;
    public int health;
    public Sprite image;

    public Leader(string name, string description, int health, Sprite image)
    {
        this.name = name;
        this.description = description;
        this.health = health;
        this.image = image;
    }


}
