using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTo : MonoBehaviour
{

    public void ResetGame()
    {
        if (LevelGenerator.isALevelInfinite){
            PlayerPrefs.SetInt("AdAlreadyWatched", 0);
            ScoreManager.Instance.ResetScore();
        }
        PlayerBehaviour.isDead = false;
        TimeManager.Instance.StopSlowmotion();
    }
    public void Return(string from)
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom", from);
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
