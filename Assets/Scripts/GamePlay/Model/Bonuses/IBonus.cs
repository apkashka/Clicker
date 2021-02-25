using UnityEngine;

public interface IBonus 
{
    Sprite Icon { get; set; }
    float Duration { get; set; }
    void Apply(ClickerModel model);
    void Undo(ClickerModel model);
}
