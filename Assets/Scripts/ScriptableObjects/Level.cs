using UnityEngine;

[CreateAssetMenu(menuName ="Clicker/LevelData")]
public class Level : ScriptableObject
{
    public string key;
    public Sprite backgroundSprite;
    public Sprite targetSpite;
    public int clicksToWin;
    public LevelStats LevelStats { get; set; }
}
