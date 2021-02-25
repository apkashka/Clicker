using UnityEngine;

public class DoubleClickBonus : IBonus
{
    private const int BONUS_VALUE = 1;
    public float Duration { get; set; }
    public Sprite Icon { get; set; }

    public void Apply(ClickerModel model)
    {
        model.PointsPerClick += BONUS_VALUE;
    }

    public void Undo(ClickerModel model)
    {
        model.PointsPerClick += BONUS_VALUE;
    }
}
