using UnityEngine;

public class SizeBonus : IBonus
{
    private const float BONUS_VALUE = 2;

    public float Duration { get; set; }
    public Sprite Icon { get; set ; }

    public void Apply(ClickerModel model)
    {
        model.SpriteScale *= BONUS_VALUE;
    }

    public void Undo(ClickerModel model)
    {
        model.SpriteScale /= BONUS_VALUE;
    }
}
