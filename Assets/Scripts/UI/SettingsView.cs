using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsView : MonoBehaviour
{
    public const string PREF_SOUND = "Sound";
    public const string PREF_MUSIC = "Music";

    [SerializeField] private RectTransform _settingsWindow;
    [SerializeField] private Button _showButton;
    [SerializeField] private Button _hideButton;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundToggle;
    
    private Image _backImage;

    private bool _isMusicOn = true;
    private bool _isSoundOn = true;

    public event System.Action<bool> MusicChanged;
    public event System.Action<bool> SoundChanged;
    public event System.Action ShowButtonClicked;
    public event System.Action HideButtonClicked;

    public void Init()
    {
        _musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        _soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        _showButton.onClick.AddListener(() => ShowButtonClicked?.Invoke());
        _hideButton.onClick.AddListener(() => HideButtonClicked?.Invoke());
;        _backImage = GetComponent<Image>();

        if (PlayerPrefs.HasKey(PREF_MUSIC))
        {
            var isMusicOn = PlayerPrefs.GetInt(PREF_MUSIC);
            _isMusicOn = isMusicOn == 1;
        }

        if (PlayerPrefs.HasKey(PREF_SOUND))
        {
            var isSoundOn = PlayerPrefs.GetInt(PREF_SOUND);
            _isSoundOn = isSoundOn == 1;
        }

        _musicToggle.isOn = _isMusicOn;
        _soundToggle.isOn = _isSoundOn;
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        _isMusicOn = isOn;
        PlayerPrefs.SetInt(PREF_MUSIC, _isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
        MusicChanged?.Invoke(_isMusicOn);
    }
    private void OnSoundToggleChanged(bool isOn)
    {
        _isSoundOn = isOn;
        PlayerPrefs.SetInt(PREF_SOUND, _isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
        SoundChanged?.Invoke(_isSoundOn);
    }
        
    public void ShowUp()
    {
        _backImage.raycastTarget = true;
        _hideButton.gameObject.SetActive(true);
        _showButton.gameObject.SetActive(false);

        _settingsWindow.DOAnchorPos(new Vector2(0, 0), 0.5f);

        _backImage.DOFade(1, 0.5f);
        _hideButton.image.DOFade(1, 0.5f);
        _showButton.image.DOFade(0, 0.5f);
    }

    public void Hide()
    {
        _settingsWindow.DOAnchorPos(new Vector2(0, 1500), 0.5f);

        _backImage.DOFade(0, 0.5f);
        _showButton.image.DOFade(1, 0.5f);
        _hideButton.image.DOFade(0, 0.5f);

        _hideButton.gameObject.SetActive(false);
        _showButton.gameObject.SetActive(true);
        _backImage.raycastTarget = false;
    }
}
