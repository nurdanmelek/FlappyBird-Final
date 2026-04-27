using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem birdDestroyPS;

    public void PlayBirdDestroyedParticles(Vector3 pos)    // particle sistemin nerede oluþacaðýný da belirtiyoruz
    {
        var newPS = Instantiate(birdDestroyPS);            // Yeni bir particle sistem oluþtur, onu bu pozisyona taþý ve oynat
        newPS.transform.position = pos;
        newPS.Play();
    }
}
