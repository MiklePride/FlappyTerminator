using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score;

    public event Action<int> ScoreChenged;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _score = 0;
        ScoreChenged?.Invoke(_score);
    }

    public void Add()
    {
        _score++;
        ScoreChenged?.Invoke(_score);
    }
}