using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.08f;
    public float duration = 0.15f;

    private Vector3 _originalScale;

    private void OnEnable()
    {
        _originalScale = transform.localScale;
    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = _originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, _originalScale * hoverScale, duration).setEaseOutBack();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, _originalScale, duration).setEaseOutQuad();
    }

}
