using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class Languages : Singleton<Languages>
{
    private List<Dictionary<string, string>> LanguagesContents;
    private int CurrentLanguage;

    public static event Action OnLanguageChanged;
    
    protected override void Awake()
    {
        base.Awake();
        ReadXmlLanguageDoc();
        CurrentLanguage = PlayerPrefs.GetInt("Language", 0);
    }

    private void ReadXmlLanguageDoc()
    {
        LanguagesContents = new List<Dictionary<string, string>>();
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
        OnLanguageChanged?.Invoke();
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
