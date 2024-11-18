using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _minShootDelay;
    [SerializeField] private float _maxShootDelay;
    [SerializeField] private float _bulletSpeed;

    private float _shootDelay;
    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (_minShootDelay <= 0.1f)
            _minShootDelay = 0.1f;

        if (_maxShootDelay <= _minShootDelay)
            _maxShootDelay = _minShootDelay;
    }

    private BulletPool _bullets;

    public event Action<Enemy> Returned;

    private void OnEnable()
    {
        _shootDelay = Random.Range(_minShootDelay, _maxShootDelay);
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            if (bullet.DidEnemyFire)
                return;
        }

        Returned?.Invoke(this);
    }

    private IEnumerator StartShoot()
    {
        var wait = new WaitForSeconds(_shootDelay);

        while (enabled)
        {
            yield return wait;

            var bullet = _bullets.Get();
            bullet.transform.position = _shootPoint.position;
            bullet.Shoot(_bulletSpeed, true);
        }
    }

    public void SetBulletPool(BulletPool bulletPool)
    {
        if (_bullets == null)
        {
            _bullets = bulletPool;
        }
    }

    public void Shoot()
    {
        _coroutine = StartCoroutine(StartShoot());
    }
}
