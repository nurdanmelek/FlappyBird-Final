using DG.Tweening;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public UIManager uIManager;
    
   private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f);
    }


    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(()=>gameObject.SetActive(false));  // canvas grubu gizle ve gizleme iþlemi bittiðinde objeyi tamamen kapat
    }


    public void PlayGameButtonPressed()
    {
        uIManager.PlayGameButtonPressed();
        

    }
}
