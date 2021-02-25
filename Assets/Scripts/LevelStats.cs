
[System.Serializable]
public class LevelStats
{
    public int stars;
    public PlayerData[] leaderboard;
}
[System.Serializable]
public class PlayerData
{
    public int id; 
    public string name;
    public float time;
}
