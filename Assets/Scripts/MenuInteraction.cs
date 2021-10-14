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
    public TMP_Text highScoreNumber;
    public TMP_Text scoreMultiplier;

    public void StartGame(){
        SceneManager.LoadScene(1); //1 for MainScene (the run)
    }
    public void ReloadMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SelectDifficulty(TMP_Text difficulty){
        difficultyTitle.text = difficulty.text;
        highScoreNumber.text = PlayerPrefs.GetInt("HighScore_"+Languages.FrenchToEnglish(difficulty.text), 0).ToString();
        PlayerPrefs.SetString("Difficulty", Languages.FrenchToEnglish(difficulty.text));

        switch (difficulty.text)
        {
            case "Facile":
            case "Easy":
            scoreMultiplier.text = "x1 MULTIPLIER";
            break;

            case "Normal":
            scoreMultiplier.text = "x3 MULTIPLIER";
            break;

            case "Difficile":
            case "Hard":
            scoreMultiplier.text = "x5 MULTIPLIER";
            break;

            default:break;
        }

        panelPlay.SetActive(true);
    }
    public void SelectLevel(TMP_Text level){
        PlayerPrefs.SetInt("Level",int.Parse(level.text));
        StartGame();
    }
    public void SelectInfini(){
        PlayerPrefs.SetInt("Level",0);
        StartGame();
    }
    public void OpenPlayButton(Animator animator){
        if(animator.GetBool("PlayIsOpen")){
              animator.Play("PlayDisappear"); // Close the Play Button
              animator.SetBool("PlayIsOpen",false);
              
        }else {
            animator.Play("PlayAppear"); // Open the Play Button
            animator.SetBool("PlayIsOpen",true);
        }
        
    }

}