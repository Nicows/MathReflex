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
        PlayerPrefs.SetString("CurrentDifficulty", "Hard");
        currentDifficulty = PlayerPrefs.GetString("CurrentDifficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Easy":
            scoreMultiplier = 1;
            break;

            case "Normal":
            scoreMultiplier = 3;
            break;

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
        score = 0;
    }
}
