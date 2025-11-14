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

    [Header("보스 스폰 설정")]
    [SerializeField] private int _bossSpawnInterval = 10000000; // 100만점마다
    private int _nextBossSpawnScore = 100000;

    public event Action OnBossSpawn;

    private void Start()
    {
        LoadUserData();
        _highScore = _userData.highScore;
        _highScoreTextUI.text = $"최고 점수 : {_highScore:N0}";
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

        // 보스 스폰 체크
        if (_currentScore >= _nextBossSpawnScore)
        {
            OnBossSpawn?.Invoke();
            _nextBossSpawnScore += _bossSpawnInterval;
            Debug.Log($"보스 스폰! 다음 보스는 {_nextBossSpawnScore:N0}점에 스폰됩니다.");
        }

        RefreshScore();
    }

    private void RefreshScore()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
        _highScoreTextUI.text = $"최고 점수 : {_highScore:N0}";

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
