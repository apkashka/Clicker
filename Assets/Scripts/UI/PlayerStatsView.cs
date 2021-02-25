using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerStatsView : MonoBehaviour, IComparer<PlayerStatsView>
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;
    private float _timeScore;
    public float TimeScore
    {
        get
        {
            return _timeScore;
        }
        set
        {
            _timeScore = value;
            _score.text = _timeScore.ToString("F1");
        }
    }

    public void UpdateData(PlayerData data)
    {
        TimeScore = data.time;
        _name.text = data.name;
    }

    public int Compare(PlayerStatsView x, PlayerStatsView y)
    {
        return x.TimeScore >= y.TimeScore ? 1 : -1;
    }


}
