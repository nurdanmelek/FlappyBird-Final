using UnityEngine;

public class GamePipeMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float destroyX = -15f;

    private static bool _canMove = true;

    private void Update()
    {
        if (!_canMove)
            return;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    public static void StopMovement()
    {
        _canMove = false;
    }

    public static void StartMovement()
    {
        _canMove = true;
    }
}