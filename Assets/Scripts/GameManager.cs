using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string JSON_NAME = "data";
    private const string PLAYER_NAME = "player";
    private const int PLAYER_ID = 12345;
    [SerializeField] private Level[] _levels;
    [SerializeField] private Sprite[] _bonusSprites;
    [SerializeField] private ClickerMenu _menu;
    [SerializeField] private LevelInfoView _levelInfoView;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private ClickerView _clickerView;
    [SerializeField] private AudioSource _musicSource;

    private GameData _gameData;
    private ClickerModel _model;
    private ClickerController _controller;
    private Level _currentLevel;

    private bool _isPlaying;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        var data = JsonParser.ParseFile<Dictionary<string, LevelStats>>(JSON_NAME);
        var model = new GameData(_levels, data);
        _gameData = model;

        _menu.Init(_gameData.Levels);
        _menu.LevelClicked += OnLevelClicked;

        _levelInfoView.Init();
        _levelInfoView.HideButtonCLicked += HideLevelInfo;

        _settingsView.MusicChanged += OnMusicChanged;
        _settingsView.SoundChanged += OnSoundChanged;
        _settingsView.ShowButtonClicked += OnShowSettingsButtonClicked;
        _settingsView.HideButtonClicked += OnHideSettingsButtonClicked;
        _settingsView.Init();

        _clickerView.Init(FindObjectOfType<Camera>());
    }

    public void Update()
    {
        if (_isPlaying && _controller!=null)
        {
            _timer += Time.deltaTime;
            _clickerView.UpdateTime(_timer);
            _controller.CheckTimeBonuses();
        }
    }

    private void OnLevelClicked(Level level)
    {
        ShowLevelInfo(level);
    }

    private void OnMusicChanged(bool isOn)
    {
        _musicSource.volume = isOn ? 0.4f : 0;
        Debug.Log($"Music is on = {isOn}");
    }
    private void OnSoundChanged(bool isOn)
    {
        Debug.Log($"Sound is on = {isOn}");
    }

    private void OnStartButtonCLicked(Level level)
    {
        _levelInfoView.StartButtonClicked -= OnStartButtonCLicked;
        StartLevel(level);
    }

    private void OnShowSettingsButtonClicked()
    {
        _settingsView.ShowUp();
        _isPlaying = false;
    }

    private void OnHideSettingsButtonClicked()
    {
        _settingsView.Hide();
        if (_currentLevel != null)
        {
            _isPlaying = true;
        }
    }

    private void OnLevelEnded()
    {
        _controller.LevelEnded -= OnLevelEnded;
        _controller.BonusAppeared -= _clickerView.ShowBonusView;
        _controller.DataUpdated -= _clickerView.UpdateView;
        _controller.BonusActivated -= _clickerView.AddActiveBonus;
        _controller.BonusDeactivated -= _clickerView.RemoveActiveBonus;

        _clickerView.TargetClicked -= _controller.NextStep;
        _clickerView.BonusClicked -= _controller.ActivateRandomBonus;


        _isPlaying = true;
        if (_currentLevel == null)
        {
            Debug.LogError("No level data");
            return;
        }

        var playerDataList = new List<PlayerData>();
        playerDataList.AddRange(_currentLevel.LevelStats.leaderboard);

        bool isInLeaderboad = false;
        foreach (var playerData in playerDataList)
        {
            if (playerData.id == PLAYER_ID)
            {
                playerData.time = _timer;
                isInLeaderboad = true;
                break;
            }
        }
        if (!isInLeaderboad)
        {
            var currentPlayerData = new PlayerData();

            currentPlayerData.name = PLAYER_NAME;
            currentPlayerData.id = PLAYER_ID;
            currentPlayerData.time = _timer;

            playerDataList.Add(currentPlayerData);
        }

        _currentLevel.LevelStats.leaderboard = playerDataList.ToArray();
        ShowLevelInfo(_currentLevel);
        _menu.gameObject.SetActive(true);
        _clickerView.gameObject.SetActive(false);
        _currentLevel = null;
    }

    private void ShowLevelInfo(Level level)
    {
        _levelInfoView.gameObject.SetActive(true);
        _levelInfoView.UpdateData(level);
        _levelInfoView.StartButtonClicked += OnStartButtonCLicked;
    }
    private void HideLevelInfo()
    {
        _levelInfoView.gameObject.SetActive(false);
        _levelInfoView.StartButtonClicked -= OnStartButtonCLicked;
    }

    private void StartLevel(Level levelData)
    {
        HideLevelInfo();
        _menu.gameObject.SetActive(false);
        _clickerView.gameObject.SetActive(true);

        _currentLevel = levelData;
        var borders = new Rect(0, (Screen.height - Screen.width) / 2, Screen.width, Screen.width);
        _model = new ClickerModel(_currentLevel.clicksToWin, _currentLevel.targetSpite, _currentLevel.backgroundSprite,borders);


        var bonuses = new IBonus[] 
        {
            new SizeBonus() { Icon = _bonusSprites[0] },
            new DoubleClickBonus() { Icon = _bonusSprites[1] },
            new MovementBonus() { Icon = _bonusSprites[2]} 
        };

        _controller = new ClickerController(_model,bonuses,0.5f);
        _controller.LevelEnded += OnLevelEnded;
        _controller.BonusAppeared += _clickerView.ShowBonusView;
        _controller.DataUpdated += _clickerView.UpdateView;
        _controller.BonusActivated += _clickerView.AddActiveBonus;
        _controller.BonusDeactivated += _clickerView.RemoveActiveBonus;

        _clickerView.SetupData(_model);
        _clickerView.UpdateView();
        _clickerView.TargetClicked += _controller.NextStep;
        _clickerView.BonusClicked += _controller.ActivateRandomBonus;

        _timer = 0;
        _isPlaying = true;
    }

}
