using System.Collections;
using UnityEngine;

public class GamePipeSpawner : MonoBehaviour
{
    [Header("Pipe")]
    public GameObject gamePipePairPrefab;

    [Header("Spawn Ayarlari")]
    public float spawnDelay = 0.5f;
    public float spawnX = 12f;

    // Boru boslugunun yukari-asagi konumu
    public float minY = -2.5f;
    public float maxY = 2.5f;

    private bool _isSpawning;

    public void SpawnPipeAfterAnswer()
    {
        // Ayni cevap olayinda iki kez boru cikmasini engelle
        if (_isSpawning)
        {
            return;
        }

        StartCoroutine(SpawnPipeCoroutine());
    }

    private IEnumerator SpawnPipeCoroutine()
    {
        _isSpawning = true;

        yield return new WaitForSeconds(spawnDelay);

        SpawnPipe();

        // Yeni cevap icin tekrar hazir
        _isSpawning = false;
    }

    private void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(
            spawnX,
            randomY,
            0f
        );

        Instantiate(
            gamePipePairPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
