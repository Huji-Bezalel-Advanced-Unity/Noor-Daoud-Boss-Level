using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5f; // Speed of the player
    private Rigidbody2D _rb;
    private Vector2 _movement;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Getting the Rigidbody2D component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        // Getting input from keyboard (WASD or Arrow keys)
        _movement.x = Input.GetAxisRaw("Horizontal"); // A, D, Left Arrow, Right Arrow
        _movement.y = Input.GetAxisRaw("Vertical"); // W, S, Up Arrow, Down Arrow
    }

    
    // FixedUpdate is called at a fixed interval and is used for handling physics
    void FixedUpdate()
    {
        // Moving the player using Rigidbody2D
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }
}