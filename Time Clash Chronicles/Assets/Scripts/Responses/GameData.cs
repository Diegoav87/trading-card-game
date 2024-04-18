[System.Serializable]
public class GameData
{
    public int player1_id;
    public int player2_id;
    public int winner_id;
    public int arena_id;
    public int duration;



    public GameData(int player1_id, int player2_id, int winner_id, int arena_id, int duration)
    {
        this.player1_id = player1_id;
        this.player2_id = player2_id;
        this.winner_id = winner_id;
        this.arena_id = arena_id;
        this.duration = duration;
    }
}