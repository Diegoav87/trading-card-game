using UnityEngine;

[System.Serializable]
public class Leader
{
    public string name;
    public float health;
    public Sprite image;

    public Sprite flag;

    public Sprite border;

    public Leader(string name, float health, Sprite image, Sprite flag, Sprite border)
    {
        this.name = name;
        this.health = health;
        this.image = image;
        this.flag = flag;
        this.border = border;

    }


}
