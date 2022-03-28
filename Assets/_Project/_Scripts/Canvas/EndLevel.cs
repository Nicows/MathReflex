using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private int _levelnumber;
    private int _nextLevel;
    [SerializeField] private GameObject _endLevelUI;
    [SerializeField] private Button _buttonRetryLevel;
    [SerializeField] private Button _buttonNextLevel;

    private void Start() => ColorManager.Instance.ColorShadowsButtons(_endLevelUI);
    private void OnEnable() => PlayerBehaviour.OnPlayerWin += FinishedLevel;
    private void OnDisable() => PlayerBehaviour.OnPlayerWin -= FinishedLevel;

    public void FinishedLevel()
    {
        // TimeManager.instance.StartEndSlowmotion(0.01f, 15f);
        StartCoroutine(WaitBeforeShow(2f));
        CheckLevelCompleted();
        _nextLevel = _levelnumber + 1;
    }
    private IEnumerator WaitBeforeShow(float secondes)
    {
        yield return new WaitForSeconds(secondes);
        _endLevelUI.SetActive(true);
        yield return new WaitForSeconds(15f);
        ReturnTo.Return("Menu");
    }
    private void CheckLevelCompleted()
    {
        _levelnumber = PlayerPrefs.GetInt("Level", 0);
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        if (PlayerPrefs.GetInt("Completed_" + currentDifficulty + "_" + _levelnumber, 0) == 0)
            PlayerPrefs.SetInt("Completed_" + currentDifficulty + "_" + _levelnumber, 1);
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        if (_nextLevel == 11) ReturnTo.Return("Tables");
        else
        {
            PlayerPrefs.SetInt("Level", _nextLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
