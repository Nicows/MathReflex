using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject endLevelUI;
    private int _levelnumber;
    private int _nextLevel;
    public Button buttonRetryLevel;
    public Button buttonNextLevel;
    public ReturnTo returnTo;

    private void Start()
    {
        ColorManager.Instance.ColorShadowsButtons(endLevelUI);
    }
    public void FinishedLevel()
    {
        // TimeManager.instance.StartEndSlowmotion(0.01f, 15f);
        StartCoroutine(WaitBeforeShow(2f));
        CheckLevelCompleted();
        _nextLevel = _levelnumber + 1;
    }
    IEnumerator WaitBeforeShow(float secondes)
    {
        yield return new WaitForSeconds(secondes);
        endLevelUI.SetActive(true);
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
        if (_nextLevel == 11) returnTo.Return("Tables");
        else
        {
            PlayerPrefs.SetInt("Level", _nextLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
