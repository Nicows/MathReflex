using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [Header ("Components")]
    public TMP_Text textScore;
    public TMP_Text textScoreNumber;
    public GameObject gameOverPanel;
    public Image player;
    
    [Header ("Scripts")]
    // public RewardedAdsButton rewardedAdsButton;
    public ScoreManager scoreManager;

    private void Start()
    {
        ColorManager.Instance.ColorShadowsButtons(gameOverPanel);
        getPlayerAvatar();
        // rewardedAdsButton.LoadAd();
    }
    private void getPlayerAvatar()
    {
        string avatarUsed = PlayerPrefs.GetString("AvatarUsed", "carre");
        player.sprite = Resources.Load<Sprite>(path: "Avatars/" + avatarUsed);
    }
    public void StartGameOver()
    {
        if (LevelGenerator.isALevelInfinite) DisplayIfLevelInfinite();
        TimeManager.Instance.StartEndSlowmotion(0.01f, 15f);
        StartCoroutine(WaitBeforeShowGameOver(2f));
    }
    private void DisplayIfLevelInfinite()
    {
        scoreManager.CalculateHighScore();
        textScore.gameObject.SetActive(true);
        textScoreNumber.gameObject.SetActive(true);
        textScoreNumber.text = scoreManager.GetScore().ToString();
    }
    public void RestartGame()
    {
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ResetGame()
    {
        if (LevelGenerator.isALevelInfinite)
        {
            PlayerPrefs.SetInt("AdAlreadyWatched", 0);
            scoreManager.ResetScore();
        }
        PlayerBehaviour.isDead = false;
        TimeManager.Instance.StopSlowmotion();
    }
    
    IEnumerator WaitBeforeShowGameOver(float secondes)
    {
        yield return new WaitForSecondsRealtime(secondes);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
