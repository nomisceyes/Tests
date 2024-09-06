using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;

    public Action<Unit> OnReleased;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() => Move();

    public void SetDirection(Vector2 direction) => _direction = direction;

    private void Move()
    {
        SetFlip();

        _rigidbody.velocity = _direction * _speed;
    }

    private void SetFlip()
    {
        if (_direction.x < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit unit) == false)
            OnReleased?.Invoke(this);
    }
}