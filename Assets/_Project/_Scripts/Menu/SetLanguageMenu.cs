using UnityEngine;
using TMPro;

public class SetLanguageMenu : MonoBehaviour
{
    [Header("Language")]
    [SerializeField] private TextMeshProUGUI _textLanguage;
    [SerializeField] private TextMeshProUGUI _textSwitchTo;

    [Header("Play")]
    [SerializeField] private TextMeshProUGUI _textPlay;
    [SerializeField] private TextMeshProUGUI _textEasy;
    [SerializeField] private TextMeshProUGUI _textNormal;
    [SerializeField] private TextMeshProUGUI _textHard;
    [SerializeField] private TextMeshProUGUI _textMultipleTable;

    [Header("HighScore")]
    [SerializeField] private TextMeshProUGUI _buttonHighScore;
    [SerializeField] private TextMeshProUGUI _textEasyHighScore;
    [SerializeField] private TextMeshProUGUI _textNormalHighScore;
    [SerializeField] private TextMeshProUGUI _textHardHighScore;

    [Header("Avatar")]
    [SerializeField] private TextMeshProUGUI _textButtonAvatar;
    [SerializeField] private TextMeshProUGUI _textTitrePanelAvatar;
    [SerializeField] private TextMeshProUGUI _textButtonChangeAvatar;
    
    private void Start()
    {
        DisplayTextOnMenu();
    }
    private void DisplayTextOnMenu()
    {
        Languages.Instance.GetPropriety("easy", out var easyValue);
        Languages.Instance.GetPropriety("normal", out var normalValue);
        Languages.Instance.GetPropriety("hard", out var hardValue);


        _textPlay.text = Languages.Instance.GetPropriety("play");
        _textEasy.text = easyValue;
        _textNormal.text = normalValue;
        _textHard.text = hardValue;
        _textMultipleTable.text = Languages.Instance.GetPropriety("tables");

        _buttonHighScore.text = Languages.Instance.GetPropriety("highscore");
        _textEasyHighScore.text = easyValue;
        _textNormalHighScore.text = normalValue;
        _textHardHighScore.text = hardValue;

        Languages.Instance.GetPropriety("characters", out var avatar);
        _textButtonAvatar.text = avatar;
        _textTitrePanelAvatar.text = avatar;
        _textButtonChangeAvatar.text = Languages.Instance.GetPropriety("change");

        _textSwitchTo.text = Languages.Instance.GetPropriety("switch");
        _textLanguage.text = GetInversedLanguage();

    }
    public void changeLanguage()
    {
        var currentLanguage = Languages.Instance.GetCurrentLanguageIndex();
        if(currentLanguage == 0)
            Languages.Instance.SetCurrentLanguage(1);
        else
            Languages.Instance.SetCurrentLanguage(0);
        
        DisplayTextOnMenu();
    }
    private string GetInversedLanguage()
    {
        var currentLanguage = Languages.Instance.GetCurrentLanguageIndex();
        return (currentLanguage is 0) ? "English" : "Français";
    }

}
