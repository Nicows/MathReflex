using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

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
    public GameObject _groupHighScore;
    public TextMeshProUGUI _textScoreSlow;
    public TextMeshProUGUI _textScoreNormal;
    public TextMeshProUGUI _textScoreFast;

    [Header("Music")]
    [SerializeField] private TextMeshProUGUI _textMusicState;

    [Header("Animation")]



    private float _blendSpeed = 100f;

    [SerializeField] private Animator _animatorGroupPlay;
    private bool _isPlayOpen = false;
    private bool _blendPlayFinished = true;

    [SerializeField] private Animator _animatorGroupHighScore;
    private bool _isHighScoreOpen = false;
    private bool _blendHighScoreFinished = true;

    [SerializeField] private Animator _animatorGroupAvatar;
    private bool _isAvatarOpen = false;
    private bool _blendAvatarFinished = true;


    private void Start()
    {
        AddAllLevels();
        DisplayHighScore();
        ReturnedFrom();
        LoadMusicState();
        AnimatorBlend();
    }

    private void AnimatorBlend()
    {
        _animatorGroupPlay.SetFloat("Blend", 0f);
        _animatorGroupHighScore.SetFloat("Blend", 0f);
        _animatorGroupAvatar.SetFloat("Blend", 0f);
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
        var language = PlayerPrefs.GetInt("Language", 0);
        if (language.Equals(0))
        {
            switch (difficultyString)
            {
                case "Easy":
                    _difficultyTitle.text = "Lent";
                    break;
                case "Hard":
                    _difficultyTitle.text = "Rapide";
                    break;
                default:
                    _difficultyTitle.text = difficultyString;
                    break;
            }
        }
        else if (language.Equals(1))
        {
            switch (difficultyString)
            {
                case "Easy":
                    _difficultyTitle.text = "Slow";
                    break;
                case "Hard":
                    _difficultyTitle.text = "Fast";
                    break;
                default:
                    _difficultyTitle.text = difficultyString;
                    break;
            }
        }



        PlayerPrefs.SetString("Difficulty", difficultyString);

        foreach (GameObject level in _listLevels)
        {
            int levelNumber = int.Parse(level.GetComponentInChildren<TMP_Text>().text);
            int levelCompleted = PlayerPrefs.GetInt("Completed_" + difficultyString + "_" + levelNumber, 0);

            // Change la couleur selon la difficulté et par niveau complété
            if (levelCompleted == 1)
            {
                level.GetComponent<Image>().color = ColorManager.GetColor(difficultyString + "Completed");
            }
            else level.GetComponent<Image>().color = ColorManager.GetDifficultyColor();
            _infiniLevel.GetComponent<Image>().color = ColorManager.GetDifficultyColor();
        }
        _mainPanel.SetActive(false);
        _panelTables.SetActive(true);
    }
    public void SelectDifficulty(TextMeshProUGUI difficulty)
    {
        string difficultyname = "";
        _difficultyTitle.text = difficulty.text;

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

        foreach (GameObject level in _listLevels)
        {
            int levelNumber = int.Parse(level.GetComponentInChildren<TMP_Text>().text);
            int levelCompleted = PlayerPrefs.GetInt("Completed_" + difficultyname + "_" + levelNumber, 0);

            // Change la couleur selon la difficulté et par niveau complété
            if (levelCompleted == 1)
            {
                level.GetComponent<Image>().color = ColorManager.GetColor(difficultyname + "Completed");
            }
            else level.GetComponent<Image>().color = ColorManager.GetDifficultyColor();
            _infiniLevel.GetComponent<Image>().color = ColorManager.GetDifficultyColor();
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
        string returnedFrom = PlayerPrefs.GetString("ReturnedFrom", "Menu");
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

    private IEnumerator BlendPlayAnimator(bool isOpen)
    {
        var blendObjectif = isOpen ? .99f : 2f;
        _blendPlayFinished = false;
        var iteration = 1 / _blendSpeed;
        var currentBlend = _animatorGroupPlay.GetFloat("Blend");
        currentBlend = (currentBlend < 0.8f) ? .99f : currentBlend;
        for (float blend = currentBlend; BlendConditionOption(blend, blendObjectif, isOpen); blend = BlendIncrementOption(blend, iteration, isOpen))
        {
            _animatorGroupPlay.SetFloat("Blend", blend);
            yield return null;
        }

        _blendPlayFinished = true;
        _isPlayOpen = !isOpen;
    }
    private IEnumerator BlendHighScoreAnimator(bool isOpen)
    {
        var blendObjectif = isOpen ? .99f : 2f;
        _blendHighScoreFinished = false;
        var iteration = 1 / _blendSpeed;
        var currentBlend = _animatorGroupHighScore.GetFloat("Blend");
        currentBlend = (currentBlend < 0.8f) ? .99f : currentBlend;

        for (float blend = currentBlend; BlendConditionOption(blend, blendObjectif, isOpen); blend = BlendIncrementOption(blend, iteration, isOpen))
        {
            _animatorGroupHighScore.SetFloat("Blend", blend);
            yield return null;
        }

        _blendHighScoreFinished = true;
        _isHighScoreOpen = !isOpen;
    }
    private IEnumerator BlendAvatarAnimator(bool isOpen)
    {
        var blendObjectif = isOpen ? .99f : 2f;
        _blendAvatarFinished = false;
        var iteration = 1 / _blendSpeed;
        var currentBlend = _animatorGroupAvatar.GetFloat("Blend");
        currentBlend = (currentBlend < 0.8f) ? .99f : currentBlend;

        for (float blend = currentBlend; BlendConditionOption(blend, blendObjectif, isOpen); blend = BlendIncrementOption(blend, iteration, isOpen))
        {
            _animatorGroupAvatar.SetFloat("Blend", blend);
            yield return null;
        }

        _blendAvatarFinished = true;
        _isAvatarOpen = !isOpen;
    }
    private float BlendIncrementOption(float blend, float minus, bool isOpen) => isOpen ? blend -= minus : blend += minus;
    private bool BlendConditionOption(float blend, float blendObjectif, bool isOpen) => isOpen ? blend >= blendObjectif : blend <= blendObjectif;

    public void AnimationPlayButton()
    {
        if (!_blendPlayFinished) return;
        StartCoroutine(BlendPlayAnimator(_isPlayOpen));
    }
    public void AnimationHighScore()
    {
        if (!_blendHighScoreFinished) return;
        StartCoroutine(BlendHighScoreAnimator(_isHighScoreOpen));
    }
    public void AnimationAvatarButton()
    {
        if (!_blendAvatarFinished) return;
        StartCoroutine(BlendAvatarAnimator(_isAvatarOpen));
    }
}