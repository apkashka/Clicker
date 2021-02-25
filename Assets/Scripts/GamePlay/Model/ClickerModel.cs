using UnityEngine;
public class ClickerModel 
{
    public int ClicksToWin { get; private set; }
    public Sprite TargetSprite { get; private set; }
    public Sprite BackGroundSprite { get; private set; }
    public Rect Borders { get; private set; }
    public int ClicksCount { get; private set; }
    public int PointsPerClick { get; set; }
    public float SpriteScale { get; set; }
    public bool ObjectIsMoving { get; set; }
    private Vector2 _currentPosition;
    public bool CanTakeBonus { get; set; }
    
    public ClickerModel(int clicksToWin, Sprite target, Sprite background, Rect borders, int pointsPerClick =1, float scale = 1, int factor = 1, bool objectIsMoving = true)
    {
        ClicksToWin = clicksToWin;
        TargetSprite = target;
        BackGroundSprite = background;
        Borders = borders;
        _currentPosition = new Vector2(borders.x + borders.width / 2, borders.y + borders.height / 2);

        PointsPerClick = pointsPerClick; 
        SpriteScale = scale;
        ObjectIsMoving = objectIsMoving;
    }
    public void CountClick()
    {
        ClicksCount += PointsPerClick;
    }

    public float Progress => (float)ClicksCount / ClicksToWin;
    public bool IsOver => Progress >= 1;
    public Vector2 GetNextPosition(bool bonusPosition = false)
    {
        if (ObjectIsMoving||bonusPosition)
        {
            Vector2 halfSize = new Vector2(TargetSprite.rect.width / 2, TargetSprite.rect.height / 2);
            halfSize *= SpriteScale;
            var finalPosition = new Vector2(Random.Range(Borders.x + halfSize.x, Borders.x + Borders.width - halfSize.x),
                                            Random.Range(Borders.y + halfSize.y, Borders.y + Borders.height - halfSize.y));
            if (!bonusPosition)
            {
                _currentPosition = finalPosition;
            }
        }
        return _currentPosition;
    }

}
