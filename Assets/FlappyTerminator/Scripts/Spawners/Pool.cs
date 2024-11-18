using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private Queue<T> _objects;
    private List<T> _activeObjects;

    private void Awake()
    {
        _objects = new Queue<T>();
        _activeObjects = new List<T>();

        if (_container == null)
            _container = transform;

        if (_capacity > 0)
        {
            for (int i = 0; i < _capacity; i++)
            {
                T poolObject = Instantiate(_prefab, _container.position, Quaternion.identity);
                poolObject.gameObject.SetActive(false);
                _objects.Enqueue(poolObject);
            }
        }
    }

    protected T GetFromPool()
    {
        T poolObject;

        if (_objects.Count == 0)
        {
            poolObject = Instantiate(_prefab);
            _activeObjects.Add(poolObject);

            return poolObject;
        }
        else
        {
            poolObject = _objects.Dequeue();
            poolObject.gameObject.SetActive(true);
            _activeObjects.Add(poolObject);

            return poolObject;
        }
    }

    protected virtual void OnReleaseObject(T poolObject)
    {
        _activeObjects.Remove(poolObject);
        _objects.Enqueue(poolObject);
        poolObject.gameObject.transform.position = _container.position;
        poolObject.gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (T poolObject in _activeObjects)
        {
            poolObject.gameObject.transform.position = _container.position;
            poolObject.gameObject.SetActive(false);
        }

        _objects.Clear();
    }
}