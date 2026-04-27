using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartLevelManager()
    {
        print("restartlevelmanager");
    }

    public void RestartLevel()
    {
       

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
