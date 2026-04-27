using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CoinManager : MonoBehaviour
{
    private int _collectedCoinCount = 0;
    public Bird bird;


    public CoinUI coinUI;
    public int coinCount;
    
    public Coin coinPrefab;   // burada tanıtabilmek için bir coin scripti oluşturuyoruz.


    private Coroutine _coinSpawnCoroutine;


    public void StartCoinSpawnCoroutine()
    {
        _coinSpawnCoroutine = StartCoroutine(CoinSpawnCoroutine());
    }


    IEnumerator CoinSpawnCoroutine()
    {
        while (true)
        {
            var spawnTime = UnityEngine.Random.Range(8f, 15f);
            var spawnPos = new Vector3(UnityEngine.Random.Range(-3f, 3f), 4, 0);
            yield return new WaitForSeconds(spawnTime);
            CreateCoinAtPosition(spawnPos);
        }
    }
    public void StopCoinSpawnCoroutine()
    {
        if (_coinSpawnCoroutine != null)
        { 
            StopCoroutine(_coinSpawnCoroutine);
        }
    }

    public void CreateCoinAtPosition(Vector3 pos)    // Herhangi bir pozisyonda bir coin oluştur. Parametre olarak da bizden bir pozisyon isteyecek
    {
        var newCoin = Instantiate(coinPrefab);
        newCoin.transform.position = pos;

        newCoin.transform.localScale = Vector3.zero;
        newCoin.transform.DOScale(1, .2f).SetEase(Ease.OutBack);

    }

    public void CoinCollected()
    {
        coinCount++;

        coinUI.UpdateCoinCount(coinCount);

        _collectedCoinCount++; // coin say

        if (_collectedCoinCount >= 3)
        {
            if (bird.CurrentHealth < bird.startHealth)
            {
                bird.HealOneHeart();
                _collectedCoinCount = 0;

            }
        }
    }
}


/*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCoinAtPosition(Vector3.zero);
        }
    }*/