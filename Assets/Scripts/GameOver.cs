using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text textScore;
    public TMP_Text textScoreNumber;
    public GameObject gameOverPanel;

    public void PlayerIsDead(){
        if(LevelGenerator.isALevelInfinite){
            ScoreManager.CalculateHighScore();  
            textScoreNumber.text = "Score : " + ScoreManager.score;
            textScore.gameObject.SetActive(true);
            textScoreNumber.gameObject.SetActive(true);
        }
        gameOverPanel.SetActive(true);
        TimeManager.instance.StartSlowmotion(0.01f, 15f);
    }
    public static void ResetGame(){
        
        if(LevelGenerator.isALevelInfinite) ScoreManager.ResetScore();
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
