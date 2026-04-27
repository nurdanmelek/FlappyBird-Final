using System;
using UnityEditor;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public LoseUI loseUI;
    private void OnMouseDown()
    {
        loseUI.ReStartButtonPressed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            loseUI.ReStartButtonPressed();
        }
    }
}
