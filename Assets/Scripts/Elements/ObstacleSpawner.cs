using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class ObstacleSpawner : MonoBehaviour
{
    public GameObject pipePrefab;

    private float _minY = -4f;
    private float _maxY = 4f;

    public void Init()
    {
        // �stersen burada kameraya g�re minY/maxY hesaplat�rs�n.
        // �imdilik Inspector de�erleriyle de gidebilir.
    }


    public GameObject Spawn()
    {
        float y = UnityEngine.Random.Range(_minY, _maxY);
        Vector3 pos = new Vector3(transform.position.x, y, 0f);

        return Instantiate(pipePrefab, pos, Quaternion.identity);
    }
}
