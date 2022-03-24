using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class Languages : Singleton<Languages>
{
    private List<Dictionary<string, string>> LanguagesContents;
    public int CurrentLanguage { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        LanguagesContents = new List<Dictionary<string, string>>();
        ReadXmlLanguageDoc();
    }

    private void ReadXmlLanguageDoc()
    {
        var dictionnary = Resources.Load<TextAsset>(path: "languages");
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionnary.text);
        var languageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languageList)
        {
            var languageContent = languageValue.ChildNodes;
            var textDictio = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            {
                textDictio.Add(value.Name, value.InnerText);
            }
            LanguagesContents.Add(textDictio);
        }
    }
    public void SetCurrentLanguage(int language)
    {
        PlayerPrefs.SetInt("Language", language);
        CurrentLanguage = language;
    }
    public string GetCurrentLanguageName()
    {
        var languageName = (CurrentLanguage == 0) ? "Français" : "English";
        return languageName;
    }
    public int GetCurrentLanguageIndex() => CurrentLanguage;


    public string GetPropriety(string proprietyName)
    {
        LanguagesContents[CurrentLanguage].TryGetValue(proprietyName, out var proprietyValue);
        return proprietyValue;
    }
    public bool GetPropriety(string proprietyName, out string value)
    {
        var hasValue = LanguagesContents[CurrentLanguage].TryGetValue(proprietyName, out value);
        return hasValue;
    }
}
