using UnityEngine;
using TMPro;

public class SetLanguageLevel : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textLevelClear;
    public TextMeshProUGUI textResume;
    public TextMeshProUGUI textQuit;
    // public TextMeshProUGUI textContinueAd;

    private void Start()
    {
        SetText();
    }
    private void SetText()
    {
        // textContinueAd.text = languages.GetPropriety("restartad");
        textLevelClear.text = Languages.Instance.GetPropriety("levelclear");
        textResume.text = Languages.Instance.GetPropriety("resume");
        textQuit.text = Languages.Instance.GetPropriety("quit");
    }
}
