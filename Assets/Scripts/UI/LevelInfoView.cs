using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoView : MonoBehaviour
{
    [SerializeField] private Text _header;
    [SerializeField] private RatingView _rating;
    [SerializeField] private Button _startLevelButton;
    [SerializeField] private Button _hideButtonClicked;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private PlayerStatsView _playerStatsPref;
    private List<PlayerStatsView> _pooledPlayers = new List<PlayerStatsView>();
    private List<PlayerData> _dataList = new List<PlayerData>();

    public event System.Action<Level> StartButtonClicked;
    public event System.Action HideButtonCLicked;

    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            var temp = Instantiate(_playerStatsPref, _scrollRect.content);
            temp.gameObject.SetActive(false);
            _pooledPlayers.Add(temp);
        }
        _hideButtonClicked.onClick.AddListener(()=>HideButtonCLicked?.Invoke());
    }
    public void UpdateData(Level level)
    {

        _header.text = level.key;
        _rating.UpdateData(level.LevelStats.stars);


        foreach (var item in _pooledPlayers)
        {
            item.gameObject.SetActive(false);
        }

        _dataList.Clear();
        _dataList.AddRange(level.LevelStats.leaderboard);
        _dataList.Sort((data1, data2) => (data1.time < data2.time)?-1:1);
        
        foreach (var playerData in _dataList)
        {
            var playerView = GetPooledPlayerStats();
            playerView.UpdateData(playerData);
            playerView.gameObject.SetActive(true);
        }

        _startLevelButton.onClick.RemoveAllListeners();
        _startLevelButton.onClick.AddListener(() => StartButtonClicked?.Invoke(level));
    }

    private PlayerStatsView GetPooledPlayerStats()
    {
        for (int i = 0; i < _pooledPlayers.Count; i++)
        {
            if (!_pooledPlayers[i].gameObject.activeInHierarchy)
            {
                return _pooledPlayers[i];
            }
        }
        PlayerStatsView temp = Instantiate(_playerStatsPref,_scrollRect.content);
        temp.gameObject.SetActive(false);
        _pooledPlayers.Add(temp);
        return temp;
    }
}
