using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pipe : MonoBehaviour
{
    public bool canMoveVertically;

    public GameObject obstacle1;
    public GameObject obstacle2;

    public float moveSpeed = 3f;
    public float verticalAmplitude = 3f;
    public float verticalSpeed = 2f;

    private float _startY;

    private void Start()
    {
        if (Random.value < .5f)
        {
            obstacle1.SetActive(false);
        }
        else
        {
            obstacle2.SetActive(false);
        }

        _startY = transform.position.y;

        canMoveVertically = Random.value < 0.5f;
    }


    // obstacle'larýn yukarý aþaðý hareketi için:
    void Update()
    {
        if (!canMoveVertically) return;

        float newY = _startY + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
