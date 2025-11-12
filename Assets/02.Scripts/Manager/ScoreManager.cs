using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _currentScoreTextUI;
    private int _currentScore = 0;

    private void Start()
    {
        RefreshScore();
    }
    
    public void AddScore(int score)
    {
        if (score <= 0) return;
        _currentScore += score;
        RefreshScore();
    }

    private void RefreshScore()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore}";
    }
    
}
