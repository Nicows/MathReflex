using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textScoreNumber;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Image _player;

    private void Start()
    {
        ColorManager.Instance.ColorShadowsButtons(_gameOverPanel);
        GetPlayerAvatar();
    }
    private void OnEnable() => PlayerBehaviour.OnPlayerDeath += StartGameOver;
    private void OnDisable() => PlayerBehaviour.OnPlayerDeath -= StartGameOver;
    
    private void GetPlayerAvatar()
    {
        string avatarUsed = PlayerPrefs.GetString("AvatarUsed", "carre");
        _player.sprite = Resources.Load<Sprite>(path: "Avatars/" + avatarUsed);
    }
    public void StartGameOver()
    {
        if (LevelGenerator.IsALevelInfinite) DisplayIfLevelInfinite();
        TimeManager.Instance.StartEndSlowmotion(0.01f, 15f);
        StartCoroutine(WaitBeforeShowGameOver(2f));
    }
    private void DisplayIfLevelInfinite()
    {
        ScoreManager.Instance.CalculateHighScore();
        _textScore.gameObject.SetActive(true);
        _textScoreNumber.gameObject.SetActive(true);
        _textScoreNumber.text = ScoreManager.Instance.GetScore().ToString();
    }
    public void RestartGame()
    {
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ResetGame()
    {
        if (LevelGenerator.IsALevelInfinite)
            ScoreManager.Instance.ResetScore();
        TimeManager.Instance.StopSlowmotion();
    }
    
    private IEnumerator WaitBeforeShowGameOver(float secondes)
    {
        yield return new WaitForSecondsRealtime(secondes);
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(10f);
        ReturnTo.Return("Menu");
    }

}
