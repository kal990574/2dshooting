using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _highScoreTextUI;

    private int _currentScore = 0;
    private int _highScore = 0;
    private const string HIGH_SCORE_KEY = "HighScore";

    private void Start()
    {
        LoadHighScore();
        RefreshScore();
    }

    
    public void AddScore(int score)
    {
        if (score <= 0) return;
        
        _currentScore += score;
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            SaveHighScore();
        }

        RefreshScore();
    }

    private void RefreshScore()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore}";
        _highScoreTextUI.text = $"최고 점수 : {_highScore}";
    }

    private void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
        PlayerPrefs.Save();
    }
}
