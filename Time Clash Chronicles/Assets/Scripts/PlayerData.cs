using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int player_id;
    public string username;
    public string password;
    public int wins;
    public int loses;



    public PlayerData(int player_id, string username, string password, int wins, int loses)
    {
        this.player_id = player_id;
        this.username = username;
        this.password = password;
        this.wins = wins;
        this.loses = loses;
    }
}
