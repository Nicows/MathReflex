using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTo : MonoBehaviour
{

    public void ResetGame()
    {
        if (LevelGenerator.IsALevelInfinite){
            PlayerPrefs.SetInt("AdAlreadyWatched", 0);
            ScoreManager.Instance.ResetScore();
        }
        PlayerBehaviour.IsDead = false;
        TimeManager.Instance.StopSlowmotion();
    }
    public void Return(string from)
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom", from);
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
