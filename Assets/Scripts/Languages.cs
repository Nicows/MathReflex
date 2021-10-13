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

    private void Awake() {
        Reader();
        GetText();
        DisplayText();
    }
    public void changeLanguage(TMP_Text chosenLanguage){
        switch (chosenLanguage.text)
        {
             case "Français":
            currentLanguage = 0;
            chosenLanguage.text = "English";
            break;
            case "English":
            currentLanguage = 1;
            chosenLanguage.text = "Français";
            break;
            default:break;
        }
        GetText();
        DisplayText();
    }
    public void GetText(){
        languages[currentLanguage].TryGetValue("Name", out languageName);
        languages[currentLanguage].TryGetValue("play", out play);
        // languages[currentLanguage].TryGetValue("settings", out settings);
        languages[currentLanguage].TryGetValue("characters", out characters);

        switch (currentLanguage)
        {
            case 0: textLanguage.text = "English";break;
            case 1: textLanguage.text = "Français";break;
            default:textLanguage.text = "Français";break;
        }
    }
    private void DisplayText(){
        textPlay.text = play;
        // textSettings.text = settings;
        textCharacters.text = characters;
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
                if(value.Name == "name"){
                    obj.Add(value.Name, value.InnerText);
                }
                if(value.Name == "play"){
                    obj.Add(value.Name, value.InnerText);
                }
                if(value.Name == "settings"){
                    obj.Add(value.Name, value.InnerText);
                }
                if(value.Name == "characters"){
                    obj.Add(value.Name, value.InnerText);
                } 
            }
            languages.Add(obj);
        }
    }
}
