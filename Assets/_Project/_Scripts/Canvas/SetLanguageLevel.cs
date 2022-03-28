using UnityEngine;
using TMPro;

public class SetLanguageLevel : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textLevelClear;
    [SerializeField] private TextMeshProUGUI textResume;
    [SerializeField] private TextMeshProUGUI textQuit;
    [SerializeField] private TextMeshProUGUI textTables;
    [SerializeField] private TextMeshProUGUI textStart;

    private void Start()
    {
        SetText();
    }
    private void SetText()
    {
        textLevelClear.text = Languages.Instance.GetPropriety("levelclear");
        textResume.text = Languages.Instance.GetPropriety("resume");
        textQuit.text = Languages.Instance.GetPropriety("quit");
        textStart.text = Languages.Instance.GetPropriety("start");
        var currentTable = PlayerPrefs.GetInt("Level", 0);
        if (currentTable == 0)
            textTables.text = "Infini";
        else
            textTables.text = Languages.Instance.GetPropriety("tablestart") + " " + (currentTable);
    }

}
