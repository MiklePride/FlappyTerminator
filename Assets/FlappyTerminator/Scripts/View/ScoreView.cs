using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        _scoreCounter.ScoreChenged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        _scoreCounter.ScoreChenged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreText.text = score.ToString();
    }
}
