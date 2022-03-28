using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuInteraction : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _panelTables;
    [SerializeField] private GameObject _panelAvatar;

    [Header("Panel Tables")]
    [SerializeField] private TextMeshProUGUI _difficultyTitle;
    [SerializeField] private GameObject _groupOfAllLevels;
    [SerializeField] private List<GameObject> _listLevels;
    [SerializeField] private GameObject _infiniLevel;

    [Header("High Score")]
    [SerializeField] private GameObject _groupHighScore;
    [SerializeField] private TextMeshProUGUI _textScoreSlow;
    [SerializeField] private TextMeshProUGUI _textScoreNormal;
    [SerializeField] private TextMeshProUGUI _textScoreFast;

    [Header("Music")]
    [SerializeField] private TextMeshProUGUI _textMusicState;

    private void Start()
    {
        AddAllLevels();
        DisplayHighScore();
        ReturnedFrom();
        LoadMusicState();
    }

    private void AddAllLevels()
    {
        foreach (Transform level in _groupOfAllLevels.transform)
        {
            _listLevels.Add(level.gameObject);
        }
    }

    public void SelectDifficulty(string difficultyString)
    {
        PlayerPrefs.SetString("Difficulty", difficultyString);
        var language = Languages.Instance.GetCurrentLanguageIndex();
        switch (difficultyString)
        {
            case "Easy":
                _difficultyTitle.text = language.Equals(0) ? "Lent" : "Slow";
                break;
            case "Hard":
                _difficultyTitle.text = language.Equals(0) ? "Rapide" : "Fast";
                break;
            default:
                _difficultyTitle.text = difficultyString;
                break;
        }

        foreach (GameObject level in _listLevels)
        {
            var levelNumber = int.Parse(level.GetComponentInChildren<TMP_Text>().text);
            var levelCompleted = PlayerPrefs.GetInt($"Completed_{difficultyString}_{levelNumber}", 0);
            if (level.TryGetComponent<Image>(out var levelImage))
            {
                levelImage.color = levelCompleted.Equals(1) ? ColorManager.Instance.GetColor(difficultyString + "Completed")
                    : ColorManager.Instance.GetDifficultyColor();
                _infiniLevel.GetComponent<Image>().color = ColorManager.Instance.GetDifficultyColor();
            }
        }
        _mainPanel.SetActive(false);
        _panelTables.SetActive(true);
    }

    public void SelectLevel(TextMeshProUGUI level)
    {
        PlayerPrefs.SetInt("Level", int.Parse(level.text));
        LoadMainScene();
    }
    public void SelectInfini()
    {
        PlayerPrefs.SetInt("Level", 0);
        LoadMainScene();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void DisplayHighScore()
    {
        _textScoreSlow.text = PlayerPrefs.GetInt("HighScore_Easy", 0).ToString();
        _textScoreNormal.text = PlayerPrefs.GetInt("HighScore_Normal", 0).ToString();
        _textScoreFast.text = PlayerPrefs.GetInt("HighScore_Hard", 0).ToString();
    }
    public void SelectPanelAvatar()
    {
        _mainPanel.SetActive(false);
        _panelAvatar.SetActive(true);
    }
    public void ReturnedFrom()
    {
        var returnedFrom = PlayerPrefs.GetString("ReturnedFrom", "Menu");
        switch (returnedFrom)
        {
            case "Avatars":
                _mainPanel.SetActive(false);
                _panelAvatar.SetActive(true);
                break;
            case "Tables":
                _mainPanel.SetActive(false);
                _panelTables.SetActive(true);
                SelectDifficulty(PlayerPrefs.GetString("Difficulty", "Easy"));
                break;
            case "Menu":
            default: break;
        }
        PlayerPrefs.SetString("ReturnedFrom", "Menu");
    }


    private void LoadMusicState()
    {
        var musicEnable = PlayerPrefs.GetInt("Music", 0).Equals(0) ? false : true;
        if (musicEnable)
        {
            _textMusicState.text = "ON";
            PlayerPrefs.SetInt("Music", 1);
            AudioSystem.Instance.PlayerDefaultMusic();
        }
        else
        {
            _textMusicState.text = "OFF";
            PlayerPrefs.SetInt("Music", 0);
            AudioSystem.Instance.StopMusic();
        }
    }
    public void ChangeMusicState()
    {
        var newMusic = PlayerPrefs.GetInt("Music", 0).Equals(0) ? 1 : 0;
        PlayerPrefs.SetInt("Music", newMusic);
        LoadMusicState();
    }

}