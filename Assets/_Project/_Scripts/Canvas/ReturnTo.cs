using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTo : MonoBehaviour
{
    public static void ResetGame()
    {
        if (LevelGenerator.IsALevelInfinite){
            // PlayerPrefs.SetInt("AdAlreadyWatched", 0);
            ScoreManager.Instance.ResetScore();
        }
        TimeManager.Instance.StopSlowmotion();
    }
    public static void Return(string from)
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom", from);
        SceneManager.LoadScene("MainMenu");
    }
}
