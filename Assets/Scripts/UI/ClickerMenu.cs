using UnityEngine;
using UnityEngine.UI;

public class ClickerMenu: MonoBehaviour
{
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] private LevelElement _levelPref;

    public event System.Action<Level> LevelClicked;
    public void Init(Level[] levels)
    {
        float height = _levelPref.GetComponent<RectTransform>().sizeDelta.y;
        float spacing = _scrollRect.content.GetComponent<VerticalLayoutGroup>().spacing;
        float contentWidth = _scrollRect.content.sizeDelta.x;
        float contentHeight = _scrollRect.content.sizeDelta.y;
        foreach (var level in levels)
        {
            var temp = Instantiate(_levelPref, _scrollRect.content);
            temp.Init(level);
            contentHeight += height + spacing;
            temp.DescriptionButtonClicked += OnDescriptionButtonClicked;
        }
        _scrollRect.content.sizeDelta = new Vector2(contentWidth, contentHeight);
    }
    private void OnDescriptionButtonClicked(Level level)
    {
        LevelClicked?.Invoke(level);
    }
}
