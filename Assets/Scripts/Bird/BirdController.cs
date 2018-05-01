using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float FlySpeed;

    Rigidbody2D _birdBody;
    Animator _birdAnimator;
    GameObject _colliderObject;

    void Awake()
    {
        _birdBody = GetComponent<Rigidbody2D>();
        _birdAnimator = GetComponentInChildren<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _colliderObject = other.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _colliderObject = other.gameObject;
    }

    public GameObject GetColliderObject()
    {
        var temp = _colliderObject;
        if (_colliderObject)
        {
            _colliderObject = null;
        }
        return temp;
    }

    public void Flap()
    {
        _birdBody.velocity = Vector2.zero;
        _birdBody.AddForce(Vector2.up * FlySpeed);
        _birdAnimator.SetTrigger("Flap");
    }

    public void Die()
    {
        _birdBody.velocity = Vector2.zero;
        _birdAnimator.SetTrigger("Die");
    }
}
