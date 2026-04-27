using System;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public MainMenu mainMenu;
    private void OnMouseDown()
    {
        mainMenu.PlayGameButtonPressed();
    }
}
