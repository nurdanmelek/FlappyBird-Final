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
    private bool _isCompleted;

    private string _originalText;


    public void Init(
        WordSelectionUI wordSelectionUI,
        int wordIndex,
        string wordText)
    {
        _wordSelectionUI = wordSelectionUI;
        _wordIndex = wordIndex;

        _originalText = wordText;
        wordTMP.text = wordText;

        _isSelected = false;
        _isCompleted = false;

        wordTMP.color = Color.black;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ToggleSelected);
    }


    private void ToggleSelected()
    {
        // Öğrenilmiş kelimenin yeşil görünümünü koru.
        // Ama tekrar çalışmak isterse seçilebilsin.
        _isSelected = !_isSelected;

        _wordSelectionUI.WordButtonClicked(
            _wordIndex,
            _isSelected
        );

        UpdateVisual();
    }


    public void SetSelected(bool selected)
    {
        _isSelected = selected;
        UpdateVisual();
    }


    public void SetCompleted(bool completed)
    {
        _isCompleted = completed;

        if (_isCompleted)
        {
            wordTMP.text = _originalText + " ✓";
        }
        else
        {
            wordTMP.text = _originalText;
        }

        UpdateVisual();
    }


    private void UpdateVisual()
    {
        // Öğrenilmiş kelime her zaman yeşil kalsın.
        if (_isCompleted)
        {
            wordTMP.color = Color.green;
            return;
        }

        // Henüz öğrenilmemiş fakat seçilmiş kelime
        if (_isSelected)
        {
            wordTMP.color = Color.green;
        }
        else
        {
            wordTMP.color = Color.black;
        }
    }


}
