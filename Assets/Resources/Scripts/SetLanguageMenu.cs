using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLanguageMenu : MonoBehaviour
{
    public Languages languages;

    [Header ("Language")]
    public TMP_Text textLanguage;
    public TMP_Text textSwitchTo;

    [Header ("Play")] 
    public TMP_Text textPlay;
    public TMP_Text textEasy;
    public TMP_Text textNormal;
    public TMP_Text textHard;
    public TMP_Text textMultipleTable;

    [Header ("HighScore")]
    public TMP_Text buttonHighScore;
    public TMP_Text textEasyHighScore;
    public TMP_Text textNormalHighScore;
    public TMP_Text textHardHighScore;

    [Header ("Avatar")]
    public TMP_Text TextButtonAvatar;
    public TMP_Text textTitrePanelAvatar;

    private void Start()
    {
        DisplayTextOnMenu();
    }
    private void DisplayTextOnMenu()
    {
        textPlay.text = languages.GetPlay();
        textEasy.text = languages.GetEasy();
        textNormal.text = languages.GetNormal();
        textHard.text = languages.GetHard();
        textMultipleTable.text = languages.GetMultipleTables();

        buttonHighScore.text = languages.GetHighScore();
        textEasyHighScore.text = languages.GetEasy();
        textNormalHighScore.text = languages.GetNormal();
        textHardHighScore.text = languages.GetHard();

        TextButtonAvatar.text = languages.GetCharacters();
        textTitrePanelAvatar.text = languages.GetCharacters();

        textSwitchTo.text = languages.GetSwitchTo();
        switch (languages.GetCurrentLanguage())
        {
            case 0: textLanguage.text = "English"; break;
            case 1: textLanguage.text = "Français"; break;
            default: textLanguage.text = "Français"; break;
        }
    }
    public void changeLanguage()
    {
        int currentLanguage = PlayerPrefs.GetInt("Language", 0);
        switch (currentLanguage)
        {
            case 0:
                languages.SetCurrentLanguage(1);
                textLanguage.text = "Français";
                break;
            case 1:
                languages.SetCurrentLanguage(0);
                textLanguage.text = "English";
                break;
            default: break;
        }
        PlayerPrefs.SetInt("Language", languages.GetCurrentLanguage());
        languages.GetText();
        DisplayTextOnMenu();
        AvatarManager.needToRefresh = true;
    }

}
