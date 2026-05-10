using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordButtonUI : MonoBehaviour
{
    

    public TextMeshProUGUI wordTMP;
    public Button button;

    private WordSelectionUI _wordSelectionUI;
    private int _wordIndex;
    public int WordIndex => _wordIndex;
    private bool _isSelected;

    public void Init(WordSelectionUI wordSelectionUI, int wordIndex, string wordText)
    {
        _wordSelectionUI = wordSelectionUI;
        _wordIndex = wordIndex;
        wordTMP.text = wordText;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ToggleSelected);
    }

    private void ToggleSelected()
    {
        _isSelected = !_isSelected;
        _wordSelectionUI.WordButtonClicked(_wordIndex, _isSelected);

        wordTMP.color = _isSelected ? Color.green : Color.black;
    }

    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        wordTMP.color = _isSelected ? Color.green : Color.black;
    }
}
