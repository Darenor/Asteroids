using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 500.0f;
    
    public float _maxLifetime = 10.0f;

    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this._speed);
        
        Destroy(this.gameObject, this._maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
