using System;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    private readonly List<GameObject> pipes = new();

    private ObstacleSpawner _spawner;

    private GameObject _lastCreatedPipe;

    private float _pipeSpeed;
    private float _spawnDistance;
    private float _destroyX;

    private bool _running;

    public float firstPipeDelay;


    public void Init(ObstacleSpawner obstacleSpawner, float pipeSpeed, float spawnDistance, float destroyX)
    {
        this._spawner = obstacleSpawner;
        this._pipeSpeed = pipeSpeed;
        this._spawnDistance = spawnDistance;
        this._destroyX = destroyX;
    }


    public void StartRun()
    {
        _running = true;
        Invoke(nameof(CreatePipe), firstPipeDelay); // ilk pipe
        //CreatePipe(); 
    }

    public void StopRun()
    {
        _running = false;
        CancelInvoke(nameof(CreatePipe));
    }



    public void ResetAll()
    {
        for (int i = 0; i < pipes.Count; i++)
        {
            if (pipes[i] != null) Destroy(pipes[i]);
        }
        pipes.Clear();
        _lastCreatedPipe = null;
    }
    private void Update()
    {
        if (!_running) return;

        // 1) Pipe hareket + silme
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            var p = pipes[i];
            if (p == null)
            {
                pipes.RemoveAt(i);
                continue;
            }

            p.transform.Translate(Vector2.left * _pipeSpeed * Time.deltaTime);

            if (p.transform.position.x <= _destroyX)
            {
                Destroy(p);
                pipes.RemoveAt(i);
            }
        }

        // 2) Yeni pipe �retme
        if (_lastCreatedPipe != null)
        {
            float dist = Vector2.Distance(_lastCreatedPipe.transform.position, _spawner.transform.position);
            if (dist > _spawnDistance)
            {
                CreatePipe();
            }
        }
    }

    private void CreatePipe()
    {
        var newPipe = _spawner.Spawn();
        _lastCreatedPipe = newPipe;
        pipes.Add(newPipe);
    }









    /*public void RestartPipeManager()
    {
        print("restartpipe");
    }*/
}
