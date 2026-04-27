using System;
using UnityEditor;
using UnityEngine;

public class PageButton : MonoBehaviour
{
    public WordSelectionUI wordSelectionUI;
    public int pageNo;
    private void OnMouseDown()
    {
        print(pageNo);
        wordSelectionUI.CreatePage(pageNo);
    }
}
