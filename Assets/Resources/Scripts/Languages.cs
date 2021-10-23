using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class Languages : MonoBehaviour
{
    public TextAsset dictionnary;
    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    public int currentLanguage;
    public static string languageName;
    private string play;
    private string characters;
    private string easy;
    private string normal;
    private string hard;
    private string highScore;
    private string multipleTables;
    private string switchTo;
    private string buy;
    private string select;
    private string selected;

    private void Awake() {
        ReadXmlLanguageDoc();
        GetText();
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
        languages[currentLanguage].TryGetValue("tables", out multipleTables);
        languages[currentLanguage].TryGetValue("switch", out switchTo);
        languages[currentLanguage].TryGetValue("buy", out buy);
        languages[currentLanguage].TryGetValue("select", out select);
        languages[currentLanguage].TryGetValue("selected", out selected);
    }
    private void ReadXmlLanguageDoc()
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
    public void SetCurrentLanguage(int language){
        currentLanguage = language;
    }
    public int GetCurrentLanguage(){
        return currentLanguage;
    }
    public string GetLanguageName(){
        return languageName;
    }
    public string GetPlay(){
        return play;
    }
    public string GetCharacters(){
        return characters;
    }
    public string GetEasy(){
        return easy;
    }
    public string GetNormal(){
        return normal;
    }
    public string GetHard(){
        return hard;
    }
    public string GetHighScore(){
        return highScore;
    }
    public string GetMultipleTables(){
        return multipleTables;
    }
    public string GetSwitchTo(){
        return switchTo;
    }
    public string GetBuy(){
        return buy;
    }
    public string GetSelect(){
        return select;
    }
    public string GetSelected(){
        return selected;
    }    
}
