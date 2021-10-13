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

    public string languageName;
    public int currentLanguage;

    private string play;
    private string settings;
    private string characters;

    public TMP_Text textPlay;
    public TMP_Text textSettings;
    public TMP_Text textCharacters;
    public TMP_Text textLanguage;

    public TMP_Text textEasy;
    public TMP_Text textNormal;
    public TMP_Text textHard;
    private string easy;
    private string normal;
    private string hard;

    private void Awake() {
        Reader();
        GetText();
        DisplayText();
    }
    
    public void GetText(){
        currentLanguage = PlayerPrefs.GetInt("Language", 0);
        languages[currentLanguage].TryGetValue("name", out languageName);
        languages[currentLanguage].TryGetValue("play", out play);
        // languages[currentLanguage].TryGetValue("settings", out settings);
        languages[currentLanguage].TryGetValue("characters", out characters);

        languages[currentLanguage].TryGetValue("easy", out easy);
        languages[currentLanguage].TryGetValue("normal", out normal);
        languages[currentLanguage].TryGetValue("hard", out hard);
        
    }
    private void DisplayText(){
        textPlay.text = play;
        // textSettings.text = settings;
        textCharacters.text = characters;

        
        textEasy.text = easy;
        textNormal.text = normal;
        textHard.text = hard;

        
        switch (currentLanguage)
        {
            case 0: textLanguage.text = "English";break;
            case 1: textLanguage.text = "Français";break;
            default:textLanguage.text = "Français";break;
        }
    }
    public void changeLanguage(){
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
            default:break;
        }
        PlayerPrefs.SetInt("Language", currentLanguage);
        GetText();
        DisplayText();
    }
    public static string FrenchToEnglish(string word){
        switch (word)
        {
            case "Facile":return "Easy";
            case "Difficile":return "Hard";
            default: return word;
        }
    }
    void Reader(){
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
}
