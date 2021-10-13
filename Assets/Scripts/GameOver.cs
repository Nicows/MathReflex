using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text textScore;
    public GameObject gameOverPanel;

    public void PlayerIsDead(){
        
        int score = ScoreManager.score;
        textScore.text = "Score : " + score;
        PlayerPrefs.SetInt("CurrentScore", score);
        if(score > PlayerPrefs.GetInt("HighScore", 0)){
            PlayerPrefs.SetInt("HighScore", score);
        }

        gameOverPanel.SetActive(true);
        TimeManager.instance.StartSlowmotion(0.01f, 15f);
    }

    public void RestartGame(){
        PlayerPrefs.SetInt("CurrentScore", 0);
        ScoreManager.ResetScore();
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
