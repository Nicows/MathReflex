using UnityEngine;
using TMPro;

public class SetLanguageLevel : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textLevelClear;
    public TextMeshProUGUI textResume;
    public TextMeshProUGUI textQuit;

    private void Start()
    {
        SetText();
    }
    private void SetText()
    {
        textLevelClear.text = Languages.Instance.GetPropriety("levelclear");
        textResume.text = Languages.Instance.GetPropriety("resume");
        textQuit.text = Languages.Instance.GetPropriety("quit");
    }
    
}
