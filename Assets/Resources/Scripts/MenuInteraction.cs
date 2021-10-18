using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    public static MenuInteraction Instance { get; private set; }
    public GameObject mainPanel;
    public GameObject panelAvatar;
    public GameObject panelPlay;

    public TMP_Text difficultyTitle;
    public GameObject groupLevels;
    public List<GameObject> listLevels;
    public GameObject infiniLevel;

    public GameObject groupHighScore;
    public TMP_Text scoreSlow;
    public TMP_Text scoreNormal;
    public TMP_Text scoreFast;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {

        foreach (Transform level in groupLevels.transform)
        {
            listLevels.Add(level.gameObject);
        }
        DisplayHighScore();
        ReturnedFrom();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1); //1 for MainScene (the run)
    }
    public void SelectDifficulty(string difficultyString)
    {
        if (Languages.languageName == "Français")
        {
            switch (difficultyString)
            {
                case "Easy":
                    difficultyTitle.text = "Lent";
                    break;
                case "Hard":
                    difficultyTitle.text = "Rapide";
                    break;
                default:
                    difficultyTitle.text = difficultyString;
                    break;
            }
        }
        else if (Languages.languageName == "English")
        {
            switch (difficultyString)
            {
                case "Easy":
                    difficultyTitle.text = "Slow";
                    break;
                case "Hard":
                    difficultyTitle.text = "Fast";
                    break;
                default:
                    difficultyTitle.text = difficultyString;
                    break;
            }
        }

        

        PlayerPrefs.SetString("Difficulty", difficultyString);

        foreach (GameObject level in listLevels)
        {
            int levelNumber = int.Parse(level.GetComponentInChildren<TMP_Text>().text);
            int levelCompleted = PlayerPrefs.GetInt("Completed_" + difficultyString + "_" + levelNumber, 0);

            // Change la couleur selon la difficulté et par niveau complété
            if (levelCompleted == 1)
            {
                level.GetComponent<Image>().color = ColorManager.GetColor(difficultyString + "Completed");
            }
            else level.GetComponent<Image>().color = ColorManager.GetColor(difficultyString);
            infiniLevel.GetComponent<Image>().color = ColorManager.GetColor(difficultyString);
        }
        mainPanel.SetActive(false);
        panelPlay.SetActive(true);
    }
    public void SelectDifficulty(TMP_Text difficulty)
    {
        string difficultyname = "";
        difficultyTitle.text = difficulty.text;

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

        foreach (GameObject level in listLevels)
        {
            int levelNumber = int.Parse(level.GetComponentInChildren<TMP_Text>().text);
            int levelCompleted = PlayerPrefs.GetInt("Completed_" + difficultyname + "_" + levelNumber, 0);

            // Change la couleur selon la difficulté et par niveau complété
            if (levelCompleted == 1)
            {
                level.GetComponent<Image>().color = ColorManager.GetColor(difficultyname + "Completed");
            }
            else level.GetComponent<Image>().color = ColorManager.GetColor(difficultyname);
            infiniLevel.GetComponent<Image>().color = ColorManager.GetColor(difficultyname);
        }
        mainPanel.SetActive(false);
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
    public void DisplayHighScore()
    {
        scoreSlow.text = PlayerPrefs.GetInt("HighScore_Easy", 0).ToString();
        scoreNormal.text = PlayerPrefs.GetInt("HighScore_Normal", 0).ToString();
        scoreFast.text = PlayerPrefs.GetInt("HighScore_Hard", 0).ToString();
    }
    public void SelectPanelAvatar()
    {
        mainPanel.SetActive(false);
        panelAvatar.SetActive(true);
    }
    public void ReturnedFrom()
    {
        string returnedFrom = PlayerPrefs.GetString("ReturnedFrom", "Menu");
        switch (returnedFrom)
        {
            case "Avatars":
                mainPanel.SetActive(false);
                panelAvatar.SetActive(true);
                break;
            case "Tables":
                mainPanel.SetActive(false);
                panelPlay.SetActive(true);
                SelectDifficulty(PlayerPrefs.GetString("Difficulty", "Easy"));
                break;
            case "Menu":
            default: break;
        }
        PlayerPrefs.SetString("ReturnedFrom", "Menu");
    }
}