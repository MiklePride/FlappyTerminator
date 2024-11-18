using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    private float _speed;
    private bool _didEnemyFire;
    private Quaternion _defaultRotation;

    public bool DidEnemyFire => _didEnemyFire;

    public event Action<Bullet> Removed;
    public event Action<Bullet> HitEnemy;

    private void Awake()
    {
        _defaultRotation = transform.rotation;
    }

    private void OnDisable()
    {
        _didEnemyFire = false;
        transform.rotation = _defaultRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_didEnemyFire && collision.GetComponent<Enemy>())
            return;

        if (!_didEnemyFire && collision.GetComponent<Bird>())
            return;

        if (!_didEnemyFire && collision.GetComponent<Enemy>())
            HitEnemy?.Invoke(this);

        Removed?.Invoke(this);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    public void Shoot(float speed, bool didEnemyFire = false)
    {
        _didEnemyFire = didEnemyFire;
        _speed = speed;
        float reverse = 180f;

        if (_didEnemyFire)
            transform.rotation = Quaternion.Euler(0, reverse, 0);
    }
}