using JetBrains.Annotations;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
  

    public Bird bird;
    private void OnTriggerEnter2D(Collider2D collision)
    {    


        if (collision.CompareTag("Coin"))
        {
            bird.CoinCollected();
            Destroy(collision.gameObject);
        }

    }
}
