using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinTMP;
    private CanvasGroup _canvasGroup;

    public Image coinImage;

    private float _coinTextYPos;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _coinTextYPos = coinTMP.transform.localPosition.y;
    }
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .1f);
    }

    public void UpdateCoinCount(int coinCount)
    {
        coinTMP.text = coinCount.ToString();

        coinTMP.transform.DOKill();
        coinTMP.transform.localPosition = new Vector3(coinTMP.transform.localPosition.x, _coinTextYPos, 0);
        coinTMP.transform.DOLocalMoveY(coinTMP.transform.localPosition.y + 15, .1f).SetLoops(2, LoopType.Yoyo);

        coinImage.transform.DOKill();
        coinImage.transform.localScale = Vector3.one * 1f;
        coinImage.transform.DOScale(Vector3.one * 1.5f, .1f).SetLoops(2, LoopType.Yoyo);

    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .1f).OnComplete(() => gameObject.SetActive(false));  // canvas grubu gizle ve gizleme iþlemi bittiðinde objeyi tamamen kapat
    }

}
