using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool _canTakeDamage = true;
    public float damageCooldown = 1f;


    private bool _isGameEnded;
    public int CurrentHealth => _currentHealth;

    public HealthUI healthUI;

    private AudioManager _audioManager;
    
    public CoinManager coinManager;
    public WordsManager wordsManager;
    public SpriteRenderer sprite;

    public int startHealth = 3;

    private int _currentHealth;

    public float minY = -4f;
    public float maxY = 4f;

    public GameDirector gameDirector;

    public TextMeshPro questionTMP;
    public string rightAnswer;

    private int _continuousRightAnswer;

    public int currentSelectedIndex;
    public List<int> selectedKeys;
    public List<int> rightAnswerCounts;

    private void Awake()
    {
        _audioManager = gameDirector.audioManager;
    }
    private void OnEnable()
    {
        _currentHealth = startHealth;
    }
    public void RestartBird()
    {
        _isGameEnded = false;
        _canTakeDamage = true;
        CancelInvoke(nameof(ResetDamage));
        gameObject.SetActive(true);

        selectedKeys = new List<int>(wordsManager.currentLevelKeys);

        rightAnswerCounts = new List<int>();

        for (int i = 0; i < selectedKeys.Count; i++)
        {
            rightAnswerCounts.Add(0);
        }

        currentSelectedIndex = Random.Range(0, selectedKeys.Count);

        var selectedKey = selectedKeys[currentSelectedIndex];

        questionTMP.text = wordsManager.latinWords[selectedKey];
        rightAnswer = wordsManager.turkishWords[selectedKey];
    }
    
    public void ChangeRightAnswer()
    {
        rightAnswerCounts[currentSelectedIndex] += 1;

        if (rightAnswerCounts[currentSelectedIndex] == 1)
        {
            selectedKeys.RemoveAt(currentSelectedIndex);
            rightAnswerCounts.RemoveAt(currentSelectedIndex);
        }

        if (selectedKeys.Count == 0)
        {
            gameObject.SetActive(false);
            Invoke(nameof(CompleteLevelDelayed), 0.7f);    
        }
        else
        {
            currentSelectedIndex = Random.Range(0, selectedKeys.Count);
            var selectedKey = selectedKeys[currentSelectedIndex];
            questionTMP.text = wordsManager.latinWords[selectedKey];
            rightAnswer = wordsManager.turkishWords[selectedKey];    
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isGameEnded) return;

        if (collision.CompareTag("Pipe"))
        {
            GetHit();
        }

        if (collision.CompareTag("Option"))
        {
            var option = collision.GetComponent<Option>();
            if (option.currentText == rightAnswer)
            {
                _audioManager.PlayTrueAnswerAS();
                
                option.OptionSelected(true);
                
                ChangeRightAnswer();
            }
            else
            {
                option.OptionSelected(false);
                _continuousRightAnswer = 0;
                GetHit();
            }
        }


    }

    private void CompleteLevelDelayed()
    {
        if (_isGameEnded) return;

        _isGameEnded = true;

        gameDirector.LevelCompleted();
        gameObject.SetActive(false);
    }

    public void CoinCollected()
    {
        coinManager.CoinCollected();
        _audioManager.PlayCoinAS(); // 🔊 burada çal
    }

    public void GetHit()
    {
        if (_isGameEnded) return;
        if (!_canTakeDamage) return;

        _canTakeDamage = false;
        Invoke(nameof(ResetDamage), damageCooldown);


        _currentHealth--;

        if (healthUI != null)
            healthUI.UpdateHealth(_currentHealth);

        if (_currentHealth > 0)
        {
            _audioManager.PlayImpactAS();
        }

        sprite.transform.DOKill();

        sprite.transform.DOShakePosition(
            duration: 0.50f,
            strength: new Vector3(0.25f, 0.15f, 0f),
            vibrato: 25,
            randomness: 20,
            fadeOut: true
        );

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            // 🔴 BURASI ÖNEMLİ → DestroyBird’den önce set edilmeli
            _isGameEnded = true;

            DestroyBird();
        }
    }

    private void ResetDamage()
    {
        _canTakeDamage = true;
    }
    private void DestroyBird()
    {
        _continuousRightAnswer = 0;
        _audioManager.PlayExplodeAS();

        //gameDirector.GameOver();   // pipe'ları durdurmak vb.

       
        gameDirector.OnBirdDestroyed(); // üst seviyeye haber ver
        gameObject.SetActive(false); // kuş “yok olsun”

    }


    public void HealOneHeart()
    {
        if (_currentHealth < startHealth)
        {
            _currentHealth++;
            healthUI.UpdateHealth(_currentHealth);
        }
    }
}
