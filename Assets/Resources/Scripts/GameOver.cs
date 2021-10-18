using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public TMP_Text textScore;
    public TMP_Text textScoreNumber;
    public GameObject gameOverPanel;
    public RewardedAdsButton rewardedAdsButton;
    public Button buttonContinue;
    // public TMP_Text textContinue;

    public void PlayerIsDead()
    {
        if (LevelGenerator.isALevelInfinite)
        {
            ScoreManager.CalculateHighScore();
            textScoreNumber.text = ScoreManager.score.ToString();
            textScore.gameObject.SetActive(true);
            textScoreNumber.gameObject.SetActive(true);
            if (PlayerPrefs.GetInt("AdAlreadyWatched", 0) == 0)
            {
                rewardedAdsButton.LoadAd();
                // rewardedAdsButton.gameObject.SetActive(true);
                buttonContinue.gameObject.SetActive(true);
            }
        }
        // textContinue.text = Languages.ad;
        TimeManager.instance.StartEndSlowmotion(0.01f, 15f);
        StartCoroutine(WaitBeforeShow(2f));


    }
    public static void ResetGame()
    {
        PlayerPrefs.SetInt("AdAlreadyWatched", 0);
        if (LevelGenerator.isALevelInfinite) ScoreManager.ResetScore();
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
    }

    public void RestartGame()
    {
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void ResumeGame()
    {
        PlayerPrefs.SetInt("AdAlreadyWatched", 1);
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void ReturnToMenu()
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom","Menu");
        SceneManager.LoadScene("MainMenu");
    }
    public static void ReturnToTables()
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom","Tables");
        SceneManager.LoadScene("MainMenu");
    }
    public static void ReturnToAvatars()
    {
        ResetGame();
        PlayerPrefs.SetString("ReturnedFrom","Avatars");
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitBeforeShow(float secondes)
    {

        yield return new WaitForSecondsRealtime(secondes);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
