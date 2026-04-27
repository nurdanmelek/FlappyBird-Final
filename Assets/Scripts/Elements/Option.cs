using TMPro;
using UnityEngine;

public class Option : MonoBehaviour
{
    public Gate gate;
    public TextMeshPro displayText;
    public string currentText;

    public void SetOption(string newText)
    {
        currentText = newText;
        displayText.text = newText;
    }

    public void OptionSelected(bool isRightAnswer)
    {
        gate.OptionSelected(this, isRightAnswer);
    }
}
