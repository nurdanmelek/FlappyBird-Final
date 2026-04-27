using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public WordsManager wordsManager;
    public float spawnInterval;
    public float gateSpawnDistance;

    private Coroutine _gateSpawnCoroutine;

    public Gate gatePrefab;
    
    private List<Gate> _gates = new List<Gate>();


    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void RestartGateManager()
    {
        if (_gateSpawnCoroutine != null)
        {
            StopCoroutine(_gateSpawnCoroutine);
        }
        _gateSpawnCoroutine = StartCoroutine(GateSpawnCoroutine());
    }

    public void StopGateManager()
    {
        if (_gateSpawnCoroutine != null)
        {
            StopCoroutine(_gateSpawnCoroutine);
        }

        Invoke(nameof(ClearGates), 2);
    }

    void ClearGates()
    {
        foreach (var g in _gates)
        {
            Destroy(g.gameObject);
        }
        
        _gates.Clear();
    }

    IEnumerator GateSpawnCoroutine()
    {
        while (true) 
        {
            var newGate = Instantiate(gatePrefab, transform);
            newGate.transform.position = Vector3.right * gateSpawnDistance;
            newGate.StartGate(wordsManager.ReturnTurkishWords());
            _gates.Add(newGate);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
