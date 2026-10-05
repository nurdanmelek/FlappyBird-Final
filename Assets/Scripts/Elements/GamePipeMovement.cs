using UnityEngine;

public class GamePipeMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float destroyX = -15f;

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
