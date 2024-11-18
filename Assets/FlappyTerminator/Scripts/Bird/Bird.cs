using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover), typeof(ScoreCounter), typeof(BirdCollisionHandler))]
public class Bird : MonoBehaviour, IInteractable
{
    private BirdMover _birdMover;
    private ScoreCounter _scoreCounter;
    private BirdCollisionHandler _collisionHandler;

    public event Action GameOver;

    private void Awake()
    {
        _birdMover = GetComponent<BirdMover>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _collisionHandler = GetComponent<BirdCollisionHandler>();
    }

    private void OnEnable() => _collisionHandler.CollisionEntered += OnHandleCollision;

    private void OnDestroy() => _collisionHandler.CollisionEntered -= OnHandleCollision;

    private void OnHandleCollision(IInteractable interactable) => GameOver?.Invoke();

    public void Reset()
    {
        _birdMover.Reset();
        _scoreCounter.Reset();
    }
}
