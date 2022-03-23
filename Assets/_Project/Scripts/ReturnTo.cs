using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTo : MonoBehaviour
{
    
    public ScoreManager scoreManager;

    public void ResetGame()
    {
        if (LevelGenerator.isALevelInfinite){
            PlayerPrefs.SetInt("AdAlreadyWatched", 0);
            scoreManager.ResetScore();
        }
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
    }
    public void ReturnToMenu()
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom", "Menu");
        SceneManager.LoadScene("MainMenu");
    }
    public void ReturnToTables()
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom", "Tables");
        SceneManager.LoadScene("MainMenu");
    }
    public void ReturnToAvatars()
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom", "Avatars");
        SceneManager.LoadScene("MainMenu");
    }
    
}
