using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class ObstacleSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public Bird bird;

    private float _minY = -4f;
    private float _maxY = 4f;

    public void Init()
    {
    }

    public GameObject Spawn()
    {
        float y = UnityEngine.Random.Range(_minY, _maxY);
        Vector3 pos = new Vector3(transform.position.x, y, 0f);

        GameObject newPipe = Instantiate(pipePrefab, pos, Quaternion.identity);

        Pipe pipe = newPipe.GetComponent<Pipe>();

        if (pipe != null)
        {
            pipe.StartPipe(bird);
        }

        return newPipe;
    }
}
