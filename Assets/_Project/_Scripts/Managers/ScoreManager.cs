using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : StaticInstance<ScoreManager>
{
    [Header("Text Components")]
    [SerializeField] private TMP_Text _textScore;
    [SerializeField] private TMP_Text _textCombo;

    private int _score = 0;
    private string _currentDifficulty;

    [Header("Combo")]
    private int _resultsAnswered = 0;
    private int _comboMultiplier = 1;
    private int _comboIncrementFromDifficulty;
    private int _nextComboAt = 5;

    private void Start()
    {
        GetComboFromDifficulty();
        DisplayTextIfInfinite();
    }
    private void OnEnable() => MultipleGenerator.OnAddScore += AddScore;
    private void OnDisable() => MultipleGenerator.OnAddScore -= AddScore;

    private void GetComboFromDifficulty()
    {
        _currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        switch (_currentDifficulty)
        {
            case "Easy":
                _comboMultiplier = 1;
                _comboIncrementFromDifficulty = 1;
                break;

            case "Normal":
                _comboMultiplier = 3;
                _comboIncrementFromDifficulty = 3;
                break;

            case "Hard":
                _comboMultiplier = 5;
                _comboIncrementFromDifficulty = 5;
                break;
        }
    }
    private void DisplayTextIfInfinite()
    {
        if (LevelGenerator.IsALevelInfinite)
        {
            _textScore.gameObject.SetActive(true);
            _textCombo.gameObject.SetActive(true);

            _score = PlayerPrefs.GetInt("CurrentScore", 0);
            _textScore.text = _score.ToString();
            _textCombo.text = "x" + _comboMultiplier;
        }
        else
        {
            _textScore.gameObject.SetActive(false);
            _textCombo.gameObject.SetActive(false);
        }
    }
    public void AddScore()
    {
        _score += 1 * _comboMultiplier;
        _textScore.SetText(_score.ToString());
        AddResultAnswered();
    }
    public int GetScore() => _score;
    public void ResetScore() => PlayerPrefs.SetInt("CurrentScore", 0);

    public void CalculateHighScore()
    {
        PlayerPrefs.SetInt("CurrentScore", _score);
        if (_score > PlayerPrefs.GetInt("HighScore_" + _currentDifficulty, 0))
        {
            SetHighScore();
        }
    }
    private void SetHighScore() => PlayerPrefs.SetInt("HighScore_" + _currentDifficulty, _score);

    private void AddResultAnswered()
    {
        _resultsAnswered++;
        checkNextCombo();
        _textCombo.text = $"x {_comboMultiplier}";
    }
    private void checkNextCombo()
    {
        if (_resultsAnswered < _nextComboAt) return;

        _comboMultiplier += _comboIncrementFromDifficulty;
        _nextComboAt = _resultsAnswered + 5;
    }

}
