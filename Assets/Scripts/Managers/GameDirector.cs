using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
   
    public AudioManager audioManager;
    
    public CoinManager coinManager;

    public GateManager gateManager;

    public FXManager fXManager;
    
    public MainMenu mainMenu;
    
    public UIManager uIManager;
    
    public WordsManager wordsManager;
    
    public LevelManager levelManager;

    public int currentLevel = 1;

    /*public IncrementalManager incrementalManager;*/

    public ObstacleManager obstacleManager;
    public Bird bird;

    // B�l�m� olu�tur
    // D��manlar� olu�tur
    // Oyuncuyu resetle; �rne�in b�l� bitti�inde oyuncu haritada alakas�z bir yerde duruyor olabilir; oyuncuyu haritan�n ba��na getirebilmek i�in �nemli.

   
    public ObstacleSpawner obstacleSpawner;
    
    public SeedManager seedManager;

    [Header("Pipe Settings")]
    
    private float _pipeSpeed = 3f;
    private float _spawnDistance = 20f;
    private float _destroyX = -20f;

    private void Start()
    {
        // uIManager.ShowMainMenu();
        mainMenu.Show();

        /*LoadPersistanceData();*/

        uIManager.GameStarted();
        // oyunu ba�lat
        
        
        seedManager.RandomizeSeed();
        
    }

    /*private void LoadPersistanceData()
    {
        incrementalManager.LoadPersistanceData();
    }*/



    // GameOver / Restart gibi eventlerde:
    public void LevelFailed()
    {
        gateManager.StopGateManager();
        obstacleManager.StopRun();
        coinManager.StopCoinSpawnCoroutine();
        audioManager.StopMusic();

        uIManager.LevelFailed();
    }

    public void LevelCompleted()
    {
        wordsManager.MarkCurrentWordsAsCompleted();
        wordsManager.SaveProgress();

        currentLevel++; 

        gateManager.StopGateManager();
        obstacleManager.StopRun();
        coinManager.StopCoinSpawnCoroutine();

        audioManager.StopMusic();
        audioManager.PlayWinAS();
        seedManager.RandomizeSeed();
        uIManager.ShowWinUI();
    }

    public void RestartLevel()
    {
        

        obstacleSpawner.Init(); // sadece ba��ml�l�klar� haz�rla (kamera vs)
        obstacleManager.Init(obstacleSpawner, GetPipeSpeed(), GetSpawnDistance(), _destroyX);

        obstacleManager.ResetAll();
        obstacleManager.StartRun();

        gateManager.SetSpawnInterval(7f);
        gateManager.RestartGateManager();

        bird.RestartBird();
        coinManager.StartCoinSpawnCoroutine();
        
        obstacleManager.StartRun();

        audioManager.StartMusic();
        uIManager.ShowInGameUI();
        uIManager.tapUI.Show(currentLevel);

    }

    public void OnBirdDestroyed()
    {
        LevelFailed();      // pipe�lar� durdur, inputu kes vb.

        fXManager.PlayBirdDestroyedParticles(bird.transform.position);

        // uIManager.LevelFailed();

        coinManager.StopCoinSpawnCoroutine();
    }
    public void CreateLevelData()
    {
        wordsManager.SetLevelKeys();
    }

    private float GetPipeSpeed()
    {
        return 3f + (currentLevel - 1) * 0.2f;
    }

    private float GetSpawnDistance()
    {
        return Mathf.Max(10f, 20f - (currentLevel - 1) * 0.6f);
    }
}
