using System.Collections.Generic;
using UnityEngine;
public class ClickerController
{
    private ClickerModel _model;
    private List<IBonus> _pooledBonuses;
    private float _bonusChance;
    private List<IBonus> _timeBonuses;

    public event System.Action LevelEnded;
    public event System.Action BonusAppeared;
    public event System.Action DataUpdated;
    public event System.Action<IBonus> BonusActivated;
    public event System.Action<IBonus> BonusDeactivated;

    public ClickerController(ClickerModel model, IBonus[] bonuses, float bonusChance)
    {
        _model = model;
        _pooledBonuses  = new List<IBonus>();
        _timeBonuses = new List<IBonus>();
        _pooledBonuses.AddRange(bonuses);
        _bonusChance = bonusChance;
    }

    public void NextStep()
    {
        _model.CountClick();
        if (_model.IsOver)
        {
            LevelEnded?.Invoke();
            return;
        }

        if (_model.CanTakeBonus == false && _pooledBonuses.Count>0 && _bonusChance > Random.Range(0f, 1f))
        {
            _model.CanTakeBonus = true;
            BonusAppeared?.Invoke();
        }
        DataUpdated?.Invoke();
    }

    public void ActivateRandomBonus()
    {
        int id = Random.Range(0, _pooledBonuses.Count);
        var bonus = _pooledBonuses[id];
        bonus.Apply(_model);
        _pooledBonuses.Remove(bonus);
        _model.CanTakeBonus = false;

        if (bonus.Duration > 0)
        {
            _timeBonuses.Add(bonus);
        }

        BonusActivated?.Invoke(bonus);
    }

    public void CheckTimeBonuses()
    {
        if (_timeBonuses.Count > 0)
        {
            var deleteList = new List<IBonus>();
            for (int i = 0; i < _timeBonuses.Count; i++)
            {
                if (_timeBonuses[i].Duration > 0)
                {
                    _timeBonuses[i].Duration -= Time.deltaTime;
                }
                else
                {
                    deleteList.Add(_timeBonuses[i]);
                }
            }

            foreach (var bonus in deleteList)
            {
                bonus.Undo(_model);
                BonusDeactivated?.Invoke(bonus);
                _timeBonuses.Remove(bonus);
            }
        }
    }

}
