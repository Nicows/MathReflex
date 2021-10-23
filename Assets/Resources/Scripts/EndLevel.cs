using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject endLevelUI;
    private int levelnumber;
    private int nextLevel;
    public Button buttonRetryLevel;
    public Button buttonNextLevel;
    public ReturnTo returnTo;

    private void Start()
    {
        ColorManager.ColorShadowsButtons(endLevelUI);
    }
    public void FinishedLevel()
    {
        StartCoroutine(WaitBeforeShow(2f));
        CheckLevelCompleted();
        nextLevel = levelnumber + 1;
    }
    IEnumerator WaitBeforeShow(float secondes)
    {
        yield return new WaitForSeconds(secondes);
        endLevelUI.SetActive(true);
    }
    private void CheckLevelCompleted()
    {
        levelnumber = PlayerPrefs.GetInt("Level", 0);
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        if (PlayerPrefs.GetInt("Completed_" + currentDifficulty + "_" + levelnumber, 0) == 0)
            PlayerPrefs.SetInt("Completed_" + currentDifficulty + "_" + levelnumber, 1);
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        if (nextLevel == 11) returnTo.ReturnToTables();
        else
        {
            PlayerPrefs.SetInt("Level", nextLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
