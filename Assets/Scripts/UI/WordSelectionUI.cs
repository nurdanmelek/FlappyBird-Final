using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
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
        print(pageNo);
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        _wordButtons.Clear();
        _selectedKeys.Clear();
        if (pageNo == 1)
        {
            for (int i = 0; i < wordsManager.latinWords.Count; i++)
            {
                var pos = new Vector2(-800 + i * xSpacing,0);

                if (i < xCount)
                {
                    pos = new Vector2(-800 + i * xSpacing,0);
                }
                else if (i < xCount * 2)
                {
                    pos = new Vector2(-800 + (i - xCount) * xSpacing, -200);
                }
                else if (i < xCount * 3)
                {
                    pos = new Vector2(-800 + (i - xCount * 2) * xSpacing, -200*2);
                }
                else
                {
                    return;
                }
            
                WordButtonUI newButton = Instantiate(wordButtonPrefab, contentParent);
                var rectTransform = newButton.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = pos;

                string text = wordsManager.latinWords[i] + " = " + wordsManager.turkishWords[i];

                newButton.Init(this, i, text);
                _wordButtons.Add(newButton);
            }
        }
        else if (pageNo == 2)
        {
            for (int i = 0; i < wordsManager.latinWords.Count; i++)
            {
                if (i >= 15)
                {
                    var pos = new Vector2(-800 + (i - 15) * xSpacing,0);

                    if (i - 15 < xCount)
                    {
                        pos = new Vector2(-800 + (i - 15) * xSpacing,0);
                    }
                    else if (i - 15 < xCount * 2)
                    {
                        pos = new Vector2(-800 + (i - xCount - 15) * xSpacing, -200);
                    }
                    else if (i - 15 < xCount * 3)
                    {
                        pos = new Vector2(-800 + (i - xCount * 2 - 15) * xSpacing, -200*2);
                    }
                    else
                    {
                        return;
                    }
            
                    WordButtonUI newButton = Instantiate(wordButtonPrefab, contentParent);
                    var rectTransform = newButton.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = pos;

                    string text = wordsManager.latinWords[i] + " = " + wordsManager.turkishWords[i];

                    newButton.Init(this, i, text);
                    _wordButtons.Add(newButton);
                }
            }
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
}
