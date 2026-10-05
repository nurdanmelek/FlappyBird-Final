using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public GamePipeSpawner gamePipeSpawner;
    public WordsManager wordsManager;

    [Header("Gate Ayarlari")]
    public float spawnInterval = 10f;
    public float gateSpawnDistance = 15f;

    [Header("Cevaptan Sonraki Bekleme")]
    public float nextGateDelay = 5f;

    [Header("Gate Kacirilinca Bekleme")]
    public float missedGateDelay = 0.5f;

    [Header("Guvenlik")]
    public float maxAnswerWaitTime = 12f;

    public Gate gatePrefab;

    private Coroutine _gateSpawnCoroutine;
    private List<Gate> _gates = new List<Gate>();

    private bool _answerSelected;
    private bool _gateMissed;


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

        _answerSelected = false;
        _gateMissed = false;

        _gateSpawnCoroutine = StartCoroutine(GateSpawnCoroutine());
    }


    public void StopGateManager()
    {
        if (_gateSpawnCoroutine != null)
        {
            StopCoroutine(_gateSpawnCoroutine);
            _gateSpawnCoroutine = null;
        }

        Invoke(nameof(ClearGates), 2);
    }


    private void ClearGates()
    {
        foreach (var g in _gates)
        {
            if (g != null)
            {
                Destroy(g.gameObject);
            }
        }

        _gates.Clear();
    }


    private IEnumerator GateSpawnCoroutine()
    {
        while (true)
        {
            // Yeni Gate icin durumlari sifirla
            _answerSelected = false;
            _gateMissed = false;

            // Yeni Gate olustur
            var newGate = Instantiate(gatePrefab, transform);

            newGate.transform.position =
                Vector3.right * gateSpawnDistance;

            newGate.StartGate(
                wordsManager.ReturnTurkishWords(),
                this
            );

            _gates.Add(newGate);


            // Oyuncu cevap verene, Gate'i kacirana
            // veya guvenlik suresi dolana kadar bekle.
            float waitTimer = 0f;

            while (
                !_answerSelected &&
                !_gateMissed &&
                waitTimer < maxAnswerWaitTime
            )
            {
                waitTimer += Time.deltaTime;
                yield return null;
            }


            // 1) Oyuncu bir cevap secti
            if (_answerSelected)
            {
                // Boru AnswerSelected() icinde zaten spawn edildi.
                // Borunun gelmesi/gecmesi icin bekle.
                yield return new WaitForSeconds(nextGateDelay);
            }

            // 2) Oyuncu hicbir secenegi secmeden Gate'i kacirdi
            else if (_gateMissed)
            {
                // Boru cikarma.
                // Kisa bir sure sonra yeni kelimeleri getir.
                yield return new WaitForSeconds(missedGateDelay);

                if (newGate != null)
                {
                    _gates.Remove(newGate);
                    Destroy(newGate.gameObject);
                }
            }

            // 3) Beklenmedik bir durumda guvenlik suresi doldu
            else
            {
                Debug.LogWarning(
                    "Gate zaman asimina ugradi. Yeni Gate olusturuluyor."
                );

                if (newGate != null)
                {
                    _gates.Remove(newGate);
                    Destroy(newGate.gameObject);
                }
            }
        }
    }


    public void AnswerSelected()
    {
        // Gate zaten cevaplandiysa veya kacirildiysa
        // tekrar islem yapma.
        if (_answerSelected || _gateMissed)
        {
            return;
        }

        _answerSelected = true;

        // Dogru veya yanlis fark etmez:
        // cevap verildikten sonra boru cikar.
        if (gamePipeSpawner != null)
        {
            gamePipeSpawner.SpawnPipeAfterAnswer();
        }
        else
        {
            Debug.LogWarning(
                "GateManager: GamePipeSpawner baglanmamis!"
            );
        }
    }


    public void GateMissed()
    {
        // Cevap zaten secildiyse Gate kacirilmis sayilmasin.
        if (_answerSelected || _gateMissed)
        {
            return;
        }

        _gateMissed = true;
    }
}
