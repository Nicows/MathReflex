using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text textScore;
    public GameObject gameOverPanel;

    public void PlayerIsDead(){
        ScoreManager.CalculateHighScore();
        textScore.text = "Score : " + ScoreManager.score;
        gameOverPanel.SetActive(true);
        TimeManager.instance.StartSlowmotion(0.01f, 15f);
    }
    public static void ResetGame(){
        
        ScoreManager.ResetScore();
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
    }

    public void RestartGame(){
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void ReturnToMenu(){
       ResetGame();
       SceneManager.LoadScene("MainMenu");
    }
}
