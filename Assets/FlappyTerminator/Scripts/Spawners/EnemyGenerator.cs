using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : Pool<Enemy>
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnSpeed;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnSpeed);
        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Enemy enemy;

        while (enabled)
        {
            yield return _wait;

            enemy = GetFromPool();
            enemy.SetBulletPool(_bulletPool);
            enemy.Returned += OnReleaseObject;

            if (_spawnPoints.Count == 1)
                enemy.gameObject.transform.position = _spawnPoints[0].transform.position;

            if (_spawnPoints.Count > 1)
            {
                int index = Random.Range(0, _spawnPoints.Count);
                enemy.gameObject.transform.position = _spawnPoints[index].transform.position;
            }

            enemy.Shoot();
        }
    }

    protected override void OnReleaseObject(Enemy poolObject)
    {
        poolObject.Returned -= OnReleaseObject;
        base.OnReleaseObject(poolObject);
    }
}