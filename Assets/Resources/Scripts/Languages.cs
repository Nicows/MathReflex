using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Xml;
using System.Text;


public class Languages : MonoBehaviour
{
    public TextAsset dictionnary;
    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    public static string languageName;
    public int currentLanguage;
    public TMP_Text textLanguage;

    private string play;
    private string characters;
    public TMP_Text textPlay;
    public TMP_Text textCharacters;

    public TMP_Text textEasy;
    public TMP_Text textNormal;
    public TMP_Text textHard;
    public TMP_Text textEasyHighScore;
    public TMP_Text textNormalHighScore;
    public TMP_Text textHardHighScore;
    private string easy;
    private string normal;
    private string hard;

    private string highScore;
    public TMP_Text buttonHighScore;

    public TMP_Text textMultiplTable;
    private string multiplTable;

    public TMP_Text textAvatar;
    public TMP_Text textSwitchTo;
    private string switchTo;

    private string buy;
    private string select;
    private string selected;

    private void Start() {
        Reader();
        GetText();
        DisplayText();
    }

    public void GetText()
    {
        currentLanguage = PlayerPrefs.GetInt("Language", 0);
        languages[currentLanguage].TryGetValue("name", out languageName);
        languages[currentLanguage].TryGetValue("play", out play);
        languages[currentLanguage].TryGetValue("characters", out characters);
        languages[currentLanguage].TryGetValue("easy", out easy);
        languages[currentLanguage].TryGetValue("normal", out normal);
        languages[currentLanguage].TryGetValue("hard", out hard);
        languages[currentLanguage].TryGetValue("highscore", out highScore);
        languages[currentLanguage].TryGetValue("tables", out multiplTable);
        languages[currentLanguage].TryGetValue("switch", out switchTo);
        languages[currentLanguage].TryGetValue("buy", out buy);
        languages[currentLanguage].TryGetValue("select", out select);
        languages[currentLanguage].TryGetValue("selected", out selected);

    }
    private void DisplayText()
    {
        textPlay.text = play;
        textCharacters.text = characters;

        textEasy.text = easy;
        textEasyHighScore.text = easy;
        textNormal.text = normal;
        textNormalHighScore.text = normal;
        textHard.text = hard;
        textHardHighScore.text = hard;

        buttonHighScore.text = highScore;

        textMultiplTable.text = multiplTable;
        textAvatar.text = characters;
        textSwitchTo.text = switchTo;

        switch (currentLanguage)
        {
            case 0: textLanguage.text = "English"; break;
            case 1: textLanguage.text = "Français"; break;
            default: textLanguage.text = "Français"; break;
        }
    }
    public void changeLanguage()
    {
        switch (textLanguage.text)
        {
            case "Français":
                currentLanguage = 0;
                textLanguage.text = "English";
                break;
            case "English":
                currentLanguage = 1;
                textLanguage.text = "Français";
                break;
            default: break;
        }
        PlayerPrefs.SetInt("Language", currentLanguage);
        GetText();
        DisplayText();
        AvatarManager.needToRefresh = true;
    }
    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionnary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languageList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            {
                obj.Add(value.Name, value.InnerText);
            }
            languages.Add(obj);
        }
    }
    public string GetSelect(){
        return select;
    }
    public string GetSelected(){
        return selected;
    }
    public string GetBuy(){
        return buy;
    }
    

}
