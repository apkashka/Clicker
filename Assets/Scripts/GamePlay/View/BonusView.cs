using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CircleCollider2D))]
public class BonusView : MonoBehaviour,IPointerClickHandler
{
    public event System.Action BonusClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        BonusClicked?.Invoke();
    }

}
