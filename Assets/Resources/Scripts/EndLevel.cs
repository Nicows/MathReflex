using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject endLevelUI;
    public GameObject triggerEndLevel;
    private int levelnumber;
    private int nextLevel;
    public Button buttonRetryLevel;
    public Button buttonNextLevel;

    private void Start() {
        if(LevelGenerator.isALevelInfinite == false) triggerEndLevel = GameObject.FindGameObjectWithTag("EndLevel");
        ColorManager.ColorShadowsButtons(endLevelUI);

    }
    public void FinishedLevel() {
        StartCoroutine(WaitBeforeShow(2f));
        
        levelnumber = PlayerPrefs.GetInt("Level", 0);
        string currentDifficulty = PlayerPrefs.GetString("Difficulty","Easy");
        if(PlayerPrefs.GetInt("Completed_"+currentDifficulty+"_"+levelnumber, 0) == 0)
            PlayerPrefs.SetInt("Completed_"+currentDifficulty+"_"+levelnumber, 1);
        if(levelnumber == 10) GameOver.ReturnToTables();
        nextLevel = levelnumber + 1;
    }
    public void RetryLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel(){
        if (levelnumber == 10) PlayerPrefs.SetInt("Level", 0);
        else PlayerPrefs.SetInt("Level", nextLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator  WaitBeforeShow(float secondes){

        yield return new WaitForSeconds(secondes);
        endLevelUI.SetActive(true);
    }
}
