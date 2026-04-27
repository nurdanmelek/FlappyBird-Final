using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public MainMenu mainMenu;


    public WinUI winUI;

    public LoseUI loseUI;
    public HintUI hintUI;

    public TapUI tapUI;

    public CoinUI coinUI;

    public HealthUI healthUI;

    public WordSelectionUI wordSelectionUI;


    public AudioManager audioManager;

    public SeedManager seedManager;

    public void GameStarted()
    {
        winUI.Hide();
        loseUI.Hide();
        hintUI.Hide();

        wordSelectionUI.Hide();

        HideInGameUI();
        healthUI.Hide();
    }

    public void ShowInGameUI()
    {
        tapUI.Show(1);

        coinUI.Show();
        coinUI.UpdateCoinCount(0);
        healthUI.Show(gameDirector.bird.startHealth);
    }

    public void HideInGameUI()
    {
        tapUI.Hide();

        coinUI.Hide();

        healthUI.Hide();
    }

    public void ShowMainMenu()
    {
        mainMenu.Show();
    }

    public void PlayGameButtonPressed()
    {
        mainMenu.Hide();
        wordSelectionUI.Show();
    }


    public void LevelCompleted()
    {
        winUI.Show();
        HideInGameUI();
    }

    public void LevelFailed()
    {
        // mainMenu.Hide();  olllmuyooor

        loseUI.Show(2f);
        audioManager.StopExplodeASDelayed(2f);

        HideInGameUI();
    }

    public void ReStartLevelButtonPressed()
    {
        gameDirector.CreateLevelData();
        hintUI.Show(0);

        //gameDirector.RestartLevel();

        // gameDirector.levelManager.RestartLevel();
    }


    /*public void RestartButtonPressed()
    {
        gameDirector.Restart();
    }*/
    public void HintUIButtonPressed()
    {
        hintUI.Hide();
        gameDirector.RestartLevel();
    }

    public void ShowWinUI()
    {
        winUI.Show();
    }

    public void LevelCompletedButtonPressed()
    {
        gameDirector.CreateLevelData();
        hintUI.Show(0);
    }

    public void LoadNextLevelButtonPressed()
    {
        gameDirector.RestartLevel();
    }

    public void StartSelectedWordsGame(List<int> selectedKeys)
    {
        gameDirector.currentLevel = 1;

        gameDirector.wordsManager.SetSelectedStudyKeys(selectedKeys);
        gameDirector.CreateLevelData();

        hintUI.Show(0);
    }
}
