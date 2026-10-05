using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pipe : MonoBehaviour
{
    public GameObject obstacle1;
    public GameObject obstacle2;

    private Enemy _activeEnemy;

    public void StartPipe(Bird bird)
    {
        if (Random.value < .5f)
        {
            obstacle1.SetActive(false);
            obstacle2.SetActive(true);

            _activeEnemy = obstacle2.GetComponent<Enemy>();
        }
        else
        {
            obstacle1.SetActive(true);
            obstacle2.SetActive(false);

            _activeEnemy = obstacle1.GetComponent<Enemy>();
        }

        if (_activeEnemy != null)
        {
            _activeEnemy.StartEnemy(bird);
        }
    }
}
