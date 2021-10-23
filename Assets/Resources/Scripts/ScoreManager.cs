using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header ("Text Components")]
    public TMP_Text textScore;
    public TMP_Text textCombo;
    
    private int score = 0;
    private string currentDifficulty;

    [Header ("Combo")]
    private int resultsAnswered = 0;
    private int comboMultiplier = 1;
    private int comboIncrementFromDifficulty;
    private int nextComboAt = 5;

    private void Start()
    {
        GetStatsFromDifficulty();
        DisplayTextIfInfinite();
    }
    private void GetStatsFromDifficulty()
    {
        currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        switch (currentDifficulty)
        {
            case "Easy":
                comboMultiplier = 1;
                comboIncrementFromDifficulty = 1;
                break;

            case "Normal":
                comboMultiplier = 3;
                comboIncrementFromDifficulty = 3;
                break;

            case "Hard":
                comboMultiplier = 5;
                comboIncrementFromDifficulty = 5;
                break;

            default: break;
        }
    }
    private void DisplayTextIfInfinite()
    {
        if (LevelGenerator.isALevelInfinite)
        {
            textScore.gameObject.SetActive(true);
            textCombo.gameObject.SetActive(true);
            
            score = PlayerPrefs.GetInt("CurrentScore", 0);
            textScore.text = score.ToString();
            textCombo.text = "x" + comboMultiplier;
        }
        else
        {
            textScore.gameObject.SetActive(false);
            textCombo.gameObject.SetActive(false);
        }
    }
    public void AddScore()
    {
        score += 1 * comboMultiplier;
        textScore.SetText(score.ToString());
        AddResultAnswered();
    }
    public int GetScore()
    {
        return score;
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
    }
    public void CalculateHighScore()
    {
        PlayerPrefs.SetInt("CurrentScore", score);
        if (score > PlayerPrefs.GetInt("HighScore_" + currentDifficulty, 0))
        {
            SetHighScore();
        }
    }
    private void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore_" + currentDifficulty, score);
    }
    public void AddResultAnswered()
    {
        resultsAnswered++;
        checkNextCombo();
        textCombo.text = "x" + comboMultiplier;
    }
    private void checkNextCombo(){
        if (resultsAnswered >= nextComboAt)
        {
            comboMultiplier += comboIncrementFromDifficulty;
            nextComboAt = resultsAnswered + 5;
        }
    }
    
}
