using System;
using UnityEngine;

public class BirdInput : MonoBehaviour
{
    private Rigidbody2D _rb;

    public float jumpForce = 10f;
   
   

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _rb.linearVelocity = new Vector2(0f, jumpForce);
        }
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
