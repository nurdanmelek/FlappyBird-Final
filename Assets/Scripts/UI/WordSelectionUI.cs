using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class WordSelectionUI : MonoBehaviour
{
    public UIManager uIManager;
    public WordsManager wordsManager;

    public Transform contentParent;
    public WordButtonUI wordButtonPrefab;

    private CanvasGroup _canvasGroup;
    
    private List<int> _selectedKeys = new List<int>();
    private List<WordButtonUI> _wordButtons = new List<WordButtonUI>();

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, .15f);

        CreateWordButtons();
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .15f).OnComplete(() => gameObject.SetActive(false));
    }

    private void CreateWordButtons()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        _wordButtons.Clear();
        _selectedKeys.Clear();

        for (int i = 0; i < wordsManager.latinWords.Count; i++)
        {
            WordButtonUI newButton = Instantiate(wordButtonPrefab, contentParent);

            string text = wordsManager.latinWords[i] + " = " + wordsManager.turkishWords[i];

            newButton.Init(this, i, text);
            _wordButtons.Add(newButton);
        }
    }

    public void WordButtonClicked(int wordIndex, bool isSelected)
    {
        if (isSelected)
        {
            if (!_selectedKeys.Contains(wordIndex))
            {
                _selectedKeys.Add(wordIndex);
            }
        }
        else
        {
            if (_selectedKeys.Contains(wordIndex))
            {
                _selectedKeys.Remove(wordIndex);
            }
        }
    }

    public void SelectAllButtonPressed()
    {
        _selectedKeys.Clear();

        for (int i = 0; i < wordsManager.latinWords.Count; i++)
        {
            _selectedKeys.Add(i);
            _wordButtons[i].SetSelected(true);
        }
    }

    public void GoOnButtonPressed()
    {
        if (_selectedKeys.Count < 3)
        {
            Debug.Log("En az 3 sözcük seçmelisin.");
            return;
        }

        if (_selectedKeys.Count % 3 != 0)
        {
            Debug.Log("Þimdilik 3'ün katý kadar sözcük seçmelisin.");
            return;
        }

        Hide();
        uIManager.StartSelectedWordsGame(_selectedKeys);
    }

}
