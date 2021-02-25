using UnityEngine;

public class MovementBonus: IBonus
{
    private const bool BONUS_VALUE = false;
    private const float DURATION_VALUE = 5;

    public float Duration { get; set; } = DURATION_VALUE;
    public Sprite Icon { get; set; }

    public void Apply(ClickerModel model)
    {
        model.ObjectIsMoving = BONUS_VALUE;
    }

    public void Undo(ClickerModel model)
    {
        model.ObjectIsMoving = !BONUS_VALUE;
    }

}
