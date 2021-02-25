using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingView : MonoBehaviour
{
    [SerializeField] private Image[] stars;

    public void UpdateData(int rating)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = rating > i ? Color.blue : Color.gray;
        }
    }
   
}
