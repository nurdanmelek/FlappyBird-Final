using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    public Image[] hearts;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(int currentHealth)
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f);
        UpdateHealth(currentHealth);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f)
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < currentHealth);
        }
    }
}
