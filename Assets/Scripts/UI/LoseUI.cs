using DG.Tweening;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    public UIManager uIManager;
    
    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void Show(float delay)
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f).SetDelay(delay);
    }


    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));  // canvas grubu gizle ve gizleme i�lemi bitti�inde objeyi tamamen kapat
    }


    public void ReStartButtonPressed()
    {
        uIManager.ReStartLevelButtonPressed();
        Hide();
    }
}
