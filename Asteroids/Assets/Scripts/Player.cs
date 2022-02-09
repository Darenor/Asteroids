using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    
    public float dashSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public float backwards = -1.0f;

    private GameManager Dead;
    private Rigidbody2D _rigidbody;
    private bool _dash;
    private bool _backwards;
    private float _turnDirection;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

     



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _dash = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        _backwards = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot(); 
        }

        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        if (_dash)
        {
            _rigidbody.AddForce(this.transform.up * this.dashSpeed);
        }
        
        if (_backwards)
        {
            _rigidbody.AddForce(this.transform.up * this.backwards);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;
        
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
