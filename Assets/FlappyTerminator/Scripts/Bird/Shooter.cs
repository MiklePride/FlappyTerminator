using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ScoreCounter))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletSpeed;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private ScoreCounter _counter;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _counter = GetComponent<ScoreCounter>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        var bullet = _bulletPool.Get();
        bullet.gameObject.transform.position = _shootPoint.position;
        bullet.gameObject.transform.rotation = transform.rotation;
        bullet.HitEnemy += OnHitEnemy;
        bullet.Shoot(_bulletSpeed);

        yield return _wait;

        _coroutine = null;
    }

    private void OnHitEnemy(Bullet bullet)
    {
        _counter.Add();
        bullet.HitEnemy -= OnHitEnemy;
    }
}
