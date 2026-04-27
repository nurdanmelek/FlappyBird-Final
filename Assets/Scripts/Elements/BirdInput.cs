using System;
using UnityEngine;

public class BirdInput : MonoBehaviour
{
    private Rigidbody2D _rb;

    public float jumpForce = 5f;
    public float moveSpeed = 3f;
    public float sprintMultiplier = 2f;

    private float _horizontalInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Saða-sola input al
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        // Yukarý flap
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        _rb.linearVelocity = new Vector2(_horizontalInput * currentSpeed, _rb.linearVelocity.y);
    }



}


/*private Rigidbody2D _rb;
    public float jumpForce;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);

        }
    }*/
