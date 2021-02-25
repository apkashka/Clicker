using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelElement : MonoBehaviour
{
    [SerializeField] private Text _header;
    [SerializeField] private RatingView _rating;

    public event System.Action<Level> DescriptionButtonClicked;
    public void Init(Level level)
    {
        _header.text = level.key;
        _rating.UpdateData(level.LevelStats.stars);
        GetComponent<Button>().onClick.AddListener(() => DescriptionButtonClicked?.Invoke(level));
    }

}
