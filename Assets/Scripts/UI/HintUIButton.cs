using System;
using UnityEngine;

public class HintUIButton : MonoBehaviour
{
    public UIManager uIManager;
    private void OnMouseDown()
    {
        uIManager.HintUIButtonPressed();
    }
}
