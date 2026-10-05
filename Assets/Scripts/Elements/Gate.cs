using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gate : MonoBehaviour
{
    private GateManager _gateManager;

    public float speed;

    public Option option1;
    public Option option2;
    public Option option3;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;

    public SpriteRenderer rightAnswerIcon;
    public SpriteRenderer wrongAnswerIcon;

    [Header("Gate Kacirma Ayari")]
    public float missedX = -3f;

    // Gate cevaplandi veya kacirildiysa tekrar islem yapilmasin.
    private bool _gateFinished = false;


    public void StartGate(List<string> turkishWords, GateManager gateManager)
    {
        _gateManager = gateManager;

        // Yeni Gate basladiginda sifirla
        _gateFinished = false;

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
        // Gate sola dogru hareket etmeye devam eder.
        transform.position += Vector3.left * Time.deltaTime * speed;

        // Oyuncu hicbir secenegi secmeden Gate'i gecirdiyse
        if (!_gateFinished && transform.position.x < missedX)
        {
            _gateFinished = true;

            if (_gateManager != null)
            {
                _gateManager.GateMissed();
            }
        }
    }


    public void OptionSelected(Option option, bool isRightAnswer)
    {
        // Bu Gate artik cevaplandi.
        // Update icindeki GateMissed calismasin.
        _gateFinished = true;

        if (_gateManager != null)
        {
            _gateManager.AnswerSelected();
        }

        option.gameObject.SetActive(false);

        if (isRightAnswer)
        {
            rightAnswerIcon.gameObject.SetActive(true);
            rightAnswerIcon.transform.position = option.transform.position;
            rightAnswerIcon.transform.localScale = Vector3.zero;

            rightAnswerIcon.transform
                .DOScale(1, .2f)
                .SetEase(Ease.OutBack);
        }
        else
        {
            wrongAnswerIcon.gameObject.SetActive(true);
            wrongAnswerIcon.transform.position = option.transform.position;
            wrongAnswerIcon.transform.localScale = Vector3.zero;

            wrongAnswerIcon.transform
                .DOScale(1, .2f)
                .SetEase(Ease.OutBack);
        }

        option1.GetComponent<BoxCollider2D>().enabled = false;
        option2.GetComponent<BoxCollider2D>().enabled = false;
        option3.GetComponent<BoxCollider2D>().enabled = false;
    }
}
