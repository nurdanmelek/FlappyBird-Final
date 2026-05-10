using System;
using UnityEditor;
using UnityEngine;

public class PageButton : MonoBehaviour
{
    public WordSelectionUI wordSelectionUI;
    public int pageNo;

    public void PageButtonPressed()
    {
        wordSelectionUI.CreatePage(pageNo);
    }
}
