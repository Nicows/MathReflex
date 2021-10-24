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
    public Button buttonContinue;
    public Image player;
    
    [Header ("Scripts")]
    public RewardedAdsButton rewardedAdsButton;
    public ScoreManager scoreManager;

    private void Start()
    {
        ColorManager.ColorShadowsButtons(gameOverPanel);
        getPlayerAvatar();
        rewardedAdsButton.LoadAd();
    }
    private void getPlayerAvatar()
    {
        string avatarUsed = PlayerPrefs.GetString("AvatarUsed", "carre");
        player.sprite = Resources.Load<Sprite>(path: "Images/Reflexion/" + avatarUsed);
    }
    public void StartGameOver()
    {
        if (LevelGenerator.isALevelInfinite) DisplayIfLevelInfinite();
        TimeManager.instance.StartEndSlowmotion(0.01f, 15f);
        StartCoroutine(WaitBeforeShowGameOver(2f));
    }
    private void DisplayIfLevelInfinite()
    {
        scoreManager.CalculateHighScore();
        textScore.gameObject.SetActive(true);
        textScoreNumber.gameObject.SetActive(true);
        textScoreNumber.text = scoreManager.GetScore().ToString();
        CheckContinueAd();
    }
    public void CheckContinueAd()
    {
        if (PlayerPrefs.GetInt("AdAlreadyWatched", 0) == 0)
        {
            buttonContinue.gameObject.SetActive(true);
        }
    }
    private void LoadAd()
    {
        
        rewardedAdsButton.ShowAd();
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
        TimeManager.instance.StopSlowmotion();
    }
    public static void ContinueGameWithAd()
    {
        PlayerPrefs.SetInt("AdAlreadyWatched", 1);
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator WaitBeforeShowGameOver(float secondes)
    {
        yield return new WaitForSecondsRealtime(secondes);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
