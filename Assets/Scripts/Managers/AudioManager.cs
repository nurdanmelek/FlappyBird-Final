using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource winAS;
    public AudioSource trueAnswerAS;
    public AudioSource coinAS;
    public AudioSource impactAS;
    public AudioSource explodeAS;

    public List<AudioSource> musicASs;

    public void PlayImpactAS()
    {
        if (impactAS != null)
            impactAS.PlayOneShot(impactAS.clip);

    }

    public void PlayExplodeAS()
    {

        if (explodeAS != null)
            explodeAS.PlayOneShot(explodeAS.clip);

    }

    public void StopExplodeASDelayed(float delay)
    {
        Invoke(nameof(StopExplodeAS), delay);   
    }

    void StopExplodeAS()
    {
        if (explodeAS != null)
            explodeAS.Stop();
    }

    public void PlayCoinAS()
    {
        if (coinAS != null)
            coinAS.PlayOneShot(coinAS.clip);
    }

    public void PlayTrueAnswerAS()
    {
        if (trueAnswerAS != null)
            trueAnswerAS.PlayOneShot(trueAnswerAS.clip);
    }

    public void PlayWinAS()
    {
        if (winAS != null)
            winAS.PlayOneShot(winAS.clip);
    }

    public void StartMusic()
    {
        if (musicASs.Count > 0 && musicASs[0] != null)
        {
            musicASs[0].Play();
        }
    }

    public void StopMusic()
    {
        foreach (var music in musicASs)
        {
            if (music != null)
                music.Stop();
        }
    }
}
