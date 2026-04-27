using UnityEngine;

public class SeedManager : MonoBehaviour
{
    private int _currentSeed = 0;

    public void RandomizeSeed()
    {
        _currentSeed = Random.Range(1, 1000);
    }

    public int GetCurrentSeed()
    {
        print(_currentSeed);
        return _currentSeed;
    }
}
