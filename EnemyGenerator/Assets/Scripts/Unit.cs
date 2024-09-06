using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(SpriteRenderer))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _direction;

    public event Action<Unit> OnReleased;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out _) == false)
            OnReleased?.Invoke(this);
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
}