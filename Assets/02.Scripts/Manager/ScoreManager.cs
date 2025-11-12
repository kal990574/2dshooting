using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text CurrentScoreTextUI;

    private int _currentScore = 0;

    private void Start()
    {
        CurrentScoreTextUI.text = $"현재 점수 : {_currentScore}"; 
    }
}
