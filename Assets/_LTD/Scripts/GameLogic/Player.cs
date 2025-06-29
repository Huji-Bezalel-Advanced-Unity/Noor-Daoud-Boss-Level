using LTD.Core.BaseMono;
using Pool;
using UnityEngine;

public class Player : LTDBaseMono
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private float _shootCooldown = 0.5f; // Cooldown time in seconds
    private float _lastShootTime;

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

        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleShooting()
    {
        // Check if Space is pressed and if cooldown has passed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _lastShootTime + _shootCooldown)
        {
            var bullet = BulletPool.Instance.Get(); // Retrieve bullet from object pool
            bullet.transform.position = transform.position + new Vector3(0f, 1.5f, 0f); // Set bullet position slightly above the player
            bullet.Shoot(Vector3.up, "Bullet", 2000, "PlayerBullet"); // Shoot the bullet upwards
            _lastShootTime = Time.time; // Update the time of the last shot
        }
    }
    
}