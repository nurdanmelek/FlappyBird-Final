using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Bird _bird;

    private enum EnemyState
    {
        Blocking,
        Chasing,
        GivingUp
    }

    private EnemyState _state = EnemyState.Blocking;


    [Header("1 - Yol Kesme")]
    public float moveSpeed = 3f;

    // Kus bu mesafeye geldiginde
    // mikrop Y ekseninde yolunu kesmeye baslar.
    public float blockDistance = 6f;

    public float blockFollowSpeed = 3.5f;


    [Header("2 - Kovalama")]

    // Kus mikrobu gectikten sonra
    // kac saniye boyunca kovalasin?
    public float chaseDuration = 2.5f;

    // Kovalama sirasinda sola dogru hareket hizi.
    public float chaseMoveSpeed = 2f;

    // Kusun Y hareketlerini takip etme hizi.
    public float chaseFollowSpeed = 4f;

    // Kus mikrobu gectikten sonra hemen tepki vermesin.
    public float chaseDelay = 0.25f;


    [Header("3 - Pes Etme")]

    // Pes ettikten sonra sola dogru kacis hizi.
    public float giveUpSpeed = 5f;

    // Bu X degerinin altina geldiginde yok olsun.
    public float destroyX = -15f;


    [Header("Dogal Dalgalanma")]
    public float waveAmount = 0.3f;
    public float waveSpeed = 3f;


    private float _targetY;

    private float _chaseTimer;
    private float _chaseDelayTimer;


    public void StartEnemy(Bird bird)
    {
        _bird = bird;

        _targetY = transform.position.y;

        _state = EnemyState.Blocking;

        _chaseTimer = 0f;
        _chaseDelayTimer = 0f;
    }


    private void Update()
    {
        if (_bird == null)
        {
            return;
        }

        switch (_state)
        {
            case EnemyState.Blocking:
                Blocking();
                break;

            case EnemyState.Chasing:
                Chasing();
                break;

            case EnemyState.GivingUp:
                GivingUp();
                break;
        }
    }


    // ==================================================
    // 1 - KUSUN YOLUNU KES
    // ==================================================

    private void Blocking()
    {
        float birdX = _bird.transform.position.x;
        float birdY = _bird.transform.position.y;

        float enemyX = transform.position.x;


        // Mikrop sola dogru geliyor.
        float newX =
            enemyX - moveSpeed * Time.deltaTime;


        // Kusa yaklasinca Y konumunu hedefle.
        float distanceX =
            Mathf.Abs(enemyX - birdX);

        if (distanceX < blockDistance)
        {
            _targetY = birdY;
        }


        // Hafif dalgalanma.
        float wave =
            Mathf.Sin(Time.time * waveSpeed) * waveAmount;

        float desiredY =
            _targetY + wave;


        float newY = Mathf.MoveTowards(
            transform.position.y,
            desiredY,
            blockFollowSpeed * Time.deltaTime
        );


        transform.position = new Vector3(
            newX,
            newY,
            transform.position.z
        );


        // ------------------------------------------
        // KUS MIKROBU GECTI
        // ------------------------------------------

        if (birdX > transform.position.x)
        {
            _state = EnemyState.Chasing;

            _chaseTimer = chaseDuration;
            _chaseDelayTimer = chaseDelay;
        }
    }


    // ==================================================
    // 2 - KUSU KOVALA
    // ==================================================

    private void Chasing()
    {
        float birdY =
            _bird.transform.position.y;


        // ------------------------------------------
        // KISA TEPKI GECIKMESI
        // ------------------------------------------

        if (_chaseDelayTimer > 0f)
        {
            _chaseDelayTimer -= Time.deltaTime;

            // Bu sirada mikrop sola hareket etmeye devam eder.
            transform.position +=
                Vector3.left *
                moveSpeed *
                Time.deltaTime;

            return;
        }


        // ------------------------------------------
        // KOVALAMA SURESI
        // ------------------------------------------

        _chaseTimer -= Time.deltaTime;


        // Mikrop sola dogru hareket etmeye devam eder.
        float newX =
            transform.position.x -
            chaseMoveSpeed * Time.deltaTime;


        // Ama Y ekseninde kusu takip eder.
        float newY = Mathf.MoveTowards(
            transform.position.y,
            birdY,
            chaseFollowSpeed * Time.deltaTime
        );


        transform.position = new Vector3(
            newX,
            newY,
            transform.position.z
        );


        // ------------------------------------------
        // KOVALAMA BITTI
        // ------------------------------------------

        if (_chaseTimer <= 0f)
        {
            _state = EnemyState.GivingUp;
        }
    }


    // ==================================================
    // 3 - PES ET VE EKRANDAN CIK
    // ==================================================

    private void GivingUp()
    {
        // Artik kusu takip etmiyor.
        // Hizla sola dogru gidiyor.

        transform.position +=
            Vector3.left *
            giveUpSpeed *
            Time.deltaTime;


        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
