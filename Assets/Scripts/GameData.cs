using System.Collections.Generic;
public class GameData 
{
    public Level[] Levels { get; set; }
    public GameData(Level[] levels, Dictionary<string, LevelStats> levelStatsDic)
    {
        foreach (var level in levels)
        {
            level.LevelStats = levelStatsDic[level.key];
        }
        Levels = levels;
    }
}
