using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    public TMP_Text difficultyTitle;
    public GameObject panelPlay;
    public GameObject[] groupLevels ;

    public GameObject groupHighScore;
    public TMP_Text scoreSlow;
    public TMP_Text scoreNormal;
    public TMP_Text scoreFast;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1); //1 for MainScene (the run)
    }

    public void SelectDifficulty(TMP_Text difficulty)
    {
        difficultyTitle.text = difficulty.text;
        string difficultyname;
        
        switch (difficulty.text)
        {
            case "Lent":
            case "Slow":
                difficultyname = "Easy";
                break;

            case "Normal":
                difficultyname = "Normal";
                break;

            case "Rapide":
            case "Fast":
                difficultyname = "Hard";
                break;

            default:
                difficultyname = "Easy";
                break;
        }
        
        PlayerPrefs.SetString("Difficulty", difficultyname);

        foreach (GameObject level in groupLevels)
        {
            int levelNumber = int.Parse(level.GetComponentInChildren<TMP_Text>().text);
            int levelCompleted = PlayerPrefs.GetInt("Completed_"+difficultyname+"_"+ levelNumber, 0);
            if(levelCompleted == 1){
                level.GetComponent<Image>().color = ColorManager.GetColor("levelFinished");
            }
        }

        panelPlay.SetActive(true);
    }
    public void SelectLevel(TMP_Text level)
    {
        PlayerPrefs.SetInt("Level", int.Parse(level.text));
        StartGame();
    }
    public void SelectInfini()
    {
        PlayerPrefs.SetInt("Level", 0);
        StartGame();
    }
    public void OpenPlayButton(Animator animator)
    {
        if (animator.GetBool("PlayIsOpen"))
        {
            animator.Play("PlayDisappear"); // Close the Play Button
            animator.SetBool("PlayIsOpen", false);

        }
        else
        {
            animator.Play("PlayAppear"); // Open the Play Button
            animator.SetBool("PlayIsOpen", true);
        }
    }
    public void DisplayHighScore()
    {
        scoreSlow.text = PlayerPrefs.GetInt("HighScore_Easy", 0).ToString();
        scoreNormal.text = PlayerPrefs.GetInt("HighScore_Normal", 0).ToString();
        scoreFast.text = PlayerPrefs.GetInt("HighScore_Hard", 0).ToString();
        if (!groupHighScore.activeSelf)
            groupHighScore.SetActive(true);
        else 
            groupHighScore.SetActive(false);
    }

}