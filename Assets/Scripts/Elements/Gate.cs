using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gate : MonoBehaviour
{

    public float speed;
    
    public Option option1;
    public Option option2;
    public Option option3;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    
    public SpriteRenderer rightAnswerIcon;
    public SpriteRenderer wrongAnswerIcon;
    
    public void StartGate(List<string> turkishWords)
    {
        pos1 = option1.transform.localPosition;
        pos2 = option2.transform.localPosition;
        pos3 = option3.transform.localPosition;
        
        option1.SetOption(turkishWords[0]);
        option2.SetOption(turkishWords[1]);
        option3.SetOption(turkishWords[2]);
        Shuffle();
    }

    private void Shuffle()
    {
        var randomizer = Random.Range(0, 6);
        if (randomizer == 0)
        {
            option1.transform.localPosition = pos1;
            option2.transform.localPosition = pos2;
            option3.transform.localPosition = pos3;
        }
        else if (randomizer == 1)
        {
            option1.transform.localPosition = pos2;
            option2.transform.localPosition = pos1;
            option3.transform.localPosition = pos3;
        }
        else if (randomizer == 2)
        {
            option1.transform.localPosition = pos3;
            option2.transform.localPosition = pos2;
            option3.transform.localPosition = pos1;
        }
        else if (randomizer == 3)
        {
            option1.transform.localPosition = pos3;
            option2.transform.localPosition = pos1;
            option3.transform.localPosition = pos2;
        }
        else if (randomizer == 4)
        {
            option1.transform.localPosition = pos1;
            option2.transform.localPosition = pos3;
            option3.transform.localPosition = pos2;
        }
        else if (randomizer == 5)
        {
            option1.transform.localPosition = pos2;
            option2.transform.localPosition = pos3;
            option3.transform.localPosition = pos1;
        }

    }

    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }

    public void OptionSelected(Option option, bool isRightAnswer)
    {
        option.gameObject.SetActive(false);
        if (isRightAnswer)
        {
            rightAnswerIcon.gameObject.SetActive(true);
            rightAnswerIcon.transform.position = option.transform.position;
            rightAnswerIcon.transform.localScale = Vector3.zero;
            rightAnswerIcon.transform.DOScale(1, .2f).SetEase(Ease.OutBack);
        }
        else
        {
            wrongAnswerIcon.gameObject.SetActive(true);
            wrongAnswerIcon.transform.position = option.transform.position;
            wrongAnswerIcon.transform.localScale = Vector3.zero;
            wrongAnswerIcon.transform.DOScale(1, .2f).SetEase(Ease.OutBack);
        }
        option1.GetComponent<BoxCollider2D>().enabled = false;
        option2.GetComponent<BoxCollider2D>().enabled = false;
        option3.GetComponent<BoxCollider2D>().enabled = false;
    }
}
