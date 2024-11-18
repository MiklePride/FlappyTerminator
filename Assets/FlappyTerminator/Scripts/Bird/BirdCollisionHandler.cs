using System;
using UnityEngine;

public class BirdCollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionEntered;

    private void OnValidate()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            CollisionEntered?.Invoke(interactable);
        }
    }
}
