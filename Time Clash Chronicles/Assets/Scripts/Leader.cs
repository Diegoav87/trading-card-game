using UnityEngine;

[System.Serializable]
public class Leader
{
    public string name;
    public int health;
    public Sprite image;

    public Leader(string name, int health, Sprite image)
    {
        this.name = name;
        this.health = health;
        this.image = image;
    }


}
