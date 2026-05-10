using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class WordSelectionUI : MonoBehaviour
{
    public int startX = -700;
    public int startY = -60;
    public int ySpacing = 130;

    public TextMeshProUGUI selectedCountTMP;

    public UIManager uIManager;
    public WordsManager wordsManager;

    public Transform contentParent;
    public WordButtonUI wordButtonPrefab;

    private CanvasGroup _canvasGroup;
    
    private List<int> _selectedKeys = new List<int>();
    private List<WordButtonUI> _wordButtons = new List<WordButtonUI>();


    [Header("GridSettings")] 
    public int xSpacing;
    public int xCount;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, .15f);

        _selectedKeys.Clear();
        UpdateSelectedCountText();
        CreateWordButtons();
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .15f).OnComplete(() => gameObject.SetActive(false));
    }

    private void CreateWordButtons()
    {
        CreatePage(1);
    }

    public void CreatePage(int pageNo)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        _wordButtons.Clear();

        int wordsPerPage = 15;
        int startIndex = (pageNo - 1) * wordsPerPage;
        int endIndex = Mathf.Min(startIndex + wordsPerPage, wordsManager.latinWords.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            int localIndex = i - startIndex;

            Vector2 pos;

            if (localIndex < xCount)
            {
                pos = new Vector2(startX + localIndex * xSpacing, startY);
            }
            else if (localIndex < xCount * 2)
            {
                pos = new Vector2(startX + (localIndex - xCount) * xSpacing, startY - ySpacing);
            }
            else
            {
                pos = new Vector2(startX + (localIndex - xCount * 2) * xSpacing, startY - ySpacing * 2);
            }

            WordButtonUI newButton = Instantiate(wordButtonPrefab, contentParent);

            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = pos;

            string text = wordsManager.latinWords[i] + " = " + wordsManager.turkishWords[i];

            newButton.Init(this, i, text);

            if (_selectedKeys.Contains(i))
            {
                newButton.SetSelected(true);
            }

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

        UpdateSelectedCountText();
    }

    public void SelectAllButtonPressed()
    {
        foreach (WordButtonUI button in _wordButtons)
        {
            if (!_selectedKeys.Contains(button.WordIndex))
            {
                _selectedKeys.Add(button.WordIndex);
            }

            button.SetSelected(true);
        }

        UpdateSelectedCountText();
    }

    public void GoOnButtonPressed()
    {
        if (_selectedKeys.Count < 3)
        {
            Debug.Log("En az 3 s�zc�k se�melisin.");
            return;
        }

        if (_selectedKeys.Count % 3 != 0)
        {
            Debug.Log("�imdilik 3'�n kat� kadar s�zc�k se�melisin.");
            return;
        }

        Hide();
        uIManager.StartSelectedWordsGame(_selectedKeys);
    }


    private void UpdateSelectedCountText()
    {

        int count = _selectedKeys.Count;

        if (count == 0)
        {
            selectedCountTMP.text = "Seçilen: 0";
        }
        else if (count % 3 == 0)
        {
            selectedCountTMP.text = "Seçilen: " + count + " ✓";
        }
        else
        {
            selectedCountTMP.text = "Seçilen: " + count + " / 3'ün katı olmalı";
        }
    }
}
