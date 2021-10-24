using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLanguageLevel : MonoBehaviour
{
    [Header("Scripts")]
    public Languages languages;

    [Header("Text")]
    public TMP_Text textLevelClear;
    public TMP_Text textContinueAd;
    public TMP_Text textResume;
    public TMP_Text textQuit;

    private void Start()
    {
        SetText();
    }
    private void SetText()
    {
        textLevelClear.text = languages.GetLevelClear();
        textContinueAd.text = languages.GetRestartAd();
        textResume.text = languages.GetResume();
        textQuit.text = languages.GetQuit();
    }
}
