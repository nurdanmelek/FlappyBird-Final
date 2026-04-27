using DG.Tweening;
using TMPro;
using UnityEngine;

public class HintUI : MonoBehaviour
{
   public TapUI tapUI;
    
    public WordsManager wordsManager;
    public TextMeshProUGUI word1;
    public TextMeshProUGUI word2;
    public TextMeshProUGUI word3;
    
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

        var key1 = wordsManager.currentLevelKeys[0];
        var key2 = wordsManager.currentLevelKeys[1];
        var key3 = wordsManager.currentLevelKeys[2];

        word1.text = wordsManager.latinWords[key1] + " = " + wordsManager.turkishWords[key1];
        word2.text = wordsManager.latinWords[key2] + " = " + wordsManager.turkishWords[key2];
        word3.text = wordsManager.latinWords[key3] + " = " + wordsManager.turkishWords[key3];

        tapUI.Hide();
    }
    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));  // canvas grubu gizle ve gizleme i�lemi bitti�inde objeyi tamamen kapat
    }
}
