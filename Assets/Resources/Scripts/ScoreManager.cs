using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text textScore;
    public TMP_Text textCombo;
    public string currentDifficulty;
    public static int score = 0;
    public int scoreMultiplier = 1;
    public int combo = 0;
    public int comboDifficulty;
    public int nextCombo = 5;

    private void Start()
    {
        currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Easy":
                scoreMultiplier = 1;
                comboDifficulty = 1;
                break;

            case "Normal":
                scoreMultiplier = 3;
                comboDifficulty = 3;
                break;

            case "Hard":
                scoreMultiplier = 5;
                comboDifficulty = 5;
                break;

            default: break;
        }
        

        if (LevelGenerator.isALevelInfinite)
        {
            textScore.gameObject.SetActive(true);
            textCombo.gameObject.SetActive(true);
            textScore.text = PlayerPrefs.GetInt("CurrentScore", 0).ToString();
            textCombo.text = "x" + scoreMultiplier;
        }
        else
        {
            textScore.gameObject.SetActive(false);
            textCombo.gameObject.SetActive(false);
        }

    }
    public void AddScore()
    {
        score += 1 * scoreMultiplier;
        textScore.SetText(score.ToString());
        Combo();
    }
    public static void ResetScore()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        score = 0;
    }
    public static void CalculateHighScore()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        PlayerPrefs.SetInt("CurrentScore", score);
        if (score > PlayerPrefs.GetInt("HighScore_" + difficulty, 0))
        {
            PlayerPrefs.SetInt("HighScore_" + difficulty, score);
        }
    }
    public void Combo()
    {
        combo++;
        if (combo >= nextCombo)
        {
            scoreMultiplier += comboDifficulty;
            nextCombo = combo + 5;
        }
        textCombo.text = "x" + scoreMultiplier;
    }
}
