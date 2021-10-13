using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text textScore;
    public string currentDifficulty;
    public static int score = 0;
    public int scoreMultiplier = 1;

    private void Awake() {
        currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Facile":
            case "Easy":
            scoreMultiplier = 1;
            break;

            case "Normal":
            scoreMultiplier = 3;
            break;

            case "Difficile":
            case "Hard":
            scoreMultiplier = 5;
            break;

            default:break;
        }
    }
    public void AddScore()
    {
        score += 1 * scoreMultiplier;
        textScore.SetText("Score : " + score);
    }
    public static void ResetScore(){
        PlayerPrefs.SetInt("CurrentScore", 0);
        score = 0;
    }
    public static void CalculateHighScore(){
        string difficulty = PlayerPrefs.GetString("Difficulty","Easy");
        PlayerPrefs.SetInt("CurrentScore", score);
        if(score > PlayerPrefs.GetInt("HighScore_"+difficulty, 0)){
            PlayerPrefs.SetInt("HighScore_"+difficulty, score);
        }
    }
}
