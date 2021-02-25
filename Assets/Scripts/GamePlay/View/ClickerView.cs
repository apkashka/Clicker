using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerView : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private TargetView _targetView;
    [SerializeField] private BonusView _bonusView;
    [SerializeField] private Image[] _activeBonusesView;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressText;
    [SerializeField] private Text _goalText;
    [SerializeField] private SpriteRenderer _background;

    private List<IBonus> _activeBonuses;

    private ClickerModel _model;
    private Camera _cam;
    public event System.Action TargetClicked;
    public event System.Action BonusClicked;

    public void Init(Camera camera)
    {
        _cam = camera;
        _targetView.Init();
        _targetView.TargetClicked += OnTargetClicked;
        _bonusView.BonusClicked += OnBonusClicked;
    }

    private void OnTargetClicked()
    {
        TargetClicked?.Invoke();
    }
    private void OnBonusClicked()
    {
        BonusClicked?.Invoke();
        _bonusView.gameObject.SetActive(false);
    }
    public void SetupData(ClickerModel model)
    {
        _model = model;
        _activeBonuses = new List<IBonus>();
        _background.sprite = _model.BackGroundSprite;
        _targetView.UpdateSprite(_model.TargetSprite);
        _targetView.UpdateScale(Vector3.one);
        _goalText.text = _model.ClicksToWin.ToString();

        foreach (var bonus in _activeBonusesView)
        {
            bonus.gameObject.SetActive(false);
        }
    }

    public void UpdateTime(float time)
    {
        _timerText.text = time.ToString("F1");
    }
    public void UpdateView()
    {
        UpdateTargetPosition(_model.GetNextPosition());
        _progressBar.fillAmount = _model.Progress;
        _progressText.text = $"{_model.ClicksCount}/{_model.ClicksToWin}";
    }

    private void UpdateTargetPosition(Vector3 screenPosition)
    {
        var position = _cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, screenPosition.z));
        position.z = 0;
        _targetView.transform.position = position;
    }

    public void ShowBonusView()
    {        
        _bonusView.gameObject.SetActive(true);
        Vector3 screenPosition = _model.GetNextPosition(true);
        var finalPosition = _cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, screenPosition.z));
        finalPosition.z = 0;
        _bonusView.transform.position = finalPosition; 
    }

    public void AddActiveBonus(IBonus bonus)
    {
        _activeBonuses.Add(bonus);
        DisplayBonuses();
        _targetView.UpdateScale(Vector3.one * _model.SpriteScale);
    
    }
    public void RemoveActiveBonus(IBonus bonus)
    {
        _activeBonuses.Remove(bonus);
        DisplayBonuses();
        _targetView.UpdateScale(Vector3.one * _model.SpriteScale);
    }

    private void DisplayBonuses()
    {
        foreach (var view in _activeBonusesView)
        {
            view.gameObject.SetActive(false);
        }
        for (int i = 0; i < _activeBonuses.Count; i++)
        {
            _activeBonusesView[i].gameObject.SetActive(true);
            _activeBonusesView[i].sprite = _activeBonuses[i].Icon;
        }
    }
}
