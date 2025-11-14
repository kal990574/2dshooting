using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _highScoreTextUI;

    private int _currentScore = 0;
    private int _highScore = 0;
    private const string USER_DATA_KEY = "UserData";
    private UserData _userData;

    public event Action<int> OnScoreChanged;
    public int CurrentScore => _currentScore;

    private void Start()
    {
        LoadUserData();
        _highScore = _userData.highScore;
        _highScoreTextUI.text = $"{_highScore:N0}";
        //PlayerPrefs.DeleteAll();
    }

    
    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            SaveUserData();
        }

        OnScoreChanged?.Invoke(_currentScore);
        RefreshScore();
    }

    private void RefreshScore()
    {
        _currentScoreTextUI.text = $"{_currentScore:N0}";
        _highScoreTextUI.text = $"{_highScore:N0}";

        PlayScaleAnimation(_currentScoreTextUI);
        PlayScaleAnimation(_highScoreTextUI);
    }

    private void PlayScaleAnimation(Text targetText)
    {
        targetText.transform.DOScale(1.1f, 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => targetText.transform.DOScale(1.0f, 0.1f));
    }

    private void LoadUserData()
    {
        string json = PlayerPrefs.GetString(USER_DATA_KEY, string.Empty);

        if (string.IsNullOrEmpty(json))
        {
            _userData = new UserData();
        }
        else
        {
            _userData = UserData.FromJson(json);
        }
    }

    private void SaveUserData()
    {
        _userData.highScore = _highScore;
        string json = _userData.ToJson();

        PlayerPrefs.SetString(USER_DATA_KEY, json);
        PlayerPrefs.Save();
    }
}
