using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TargetView : MonoBehaviour, IPointerClickHandler
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    public event System.Action TargetClicked;

    public void Init()
    {
        if (_spriteRenderer == null ||_collider == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
        }
    }
    public void UpdateSprite(Sprite targetSprite)
    {
        _spriteRenderer.sprite = targetSprite;
        _collider.size = _spriteRenderer.sprite.bounds.size;
    }

    public void UpdateScale(Vector2 newScale)
    {
        transform.localScale = newScale;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TargetClicked?.Invoke();
    }
}
