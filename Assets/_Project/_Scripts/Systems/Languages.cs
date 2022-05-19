using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class Languages : Singleton<Languages>
{
    private List<Dictionary<string, string>> LanguagesContents;
    private int CurrentLanguage = 0;

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
    
    public string GetCurrentLanguageName() => (CurrentLanguage == 0) ? "Français" : "English"; 
    
    public int GetCurrentLanguageIndex() => CurrentLanguage;

    public string GetPropriety(string proprietyName)
    {
        if(LanguagesContents[CurrentLanguage].TryGetValue(proprietyName, out var proprietyValue)) return proprietyValue;
        else throw new Exception($"Propriety {proprietyName} not found");
    }
    public void GetPropriety(string proprietyName, out string value){
        if (!LanguagesContents[CurrentLanguage].TryGetValue(proprietyName, out value)) throw new Exception($"Propriety {proprietyName} not found");
    }

    
}
