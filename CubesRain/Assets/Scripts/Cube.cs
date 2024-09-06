using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    public static Action<Cube> OnTouched;

    private MeshRenderer _cubeMesh;
    private Rigidbody _rigidbody;
    private bool _isActive;

    private void Awake()
    {
        _cubeMesh = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Reset();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive == false && collision.gameObject.TryGetComponent<ColorChanger>(out ColorChanger colorChanger))
        {
            _cubeMesh.material.color = colorChanger.GetColor();
            OnTouched?.Invoke(this);
            _isActive = true;
        }
    }

    public void Reset()
    {
        _isActive = false;
        _cubeMesh.material.color = Color.white;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}