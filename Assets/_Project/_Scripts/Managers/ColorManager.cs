using UnityEngine;
using UnityEngine.UI;

public class ColorManager : StaticInstance<ColorManager>
{
    private Color _colorDifficulty;

    private void Start()
    {
        GetDifficultyColor();
        RefreshColorDifficultyInAllComponents();
    }
    public Color GetDifficultyColor()
    {
        var currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        _colorDifficulty = GetColor(currentDifficulty);
        return _colorDifficulty;
    }
    public Color GetColor(string colorString)
    {
        var colorHex = "#000000";
        switch (colorString)
        {
            case "Easy":
                colorHex = "#6EC6BA";
                break;
            case "Normal":
                colorHex = "#55C5FA";
                break;
            case "Hard":
                colorHex = "#DD3829";
                break;
            case "EasyCompleted":
                colorHex = "#47827A";
                break;
            case "NormalCompleted":
                colorHex = "#3F8EB3";
                break;
            case "HardCompleted":
                colorHex = "#A42318";
                break;
            default:
                colorHex = "#6EC6BA";
                break;
        }

        ColorUtility.TryParseHtmlString(colorHex, out var color);
        return color;
    }
    public void RefreshColorDifficultyInAllComponents()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("Color");

        foreach (GameObject gameobject in gameObjects)
        {
            if (gameobject.GetComponent<SpriteRenderer>())
                gameobject.GetComponent<SpriteRenderer>().color = _colorDifficulty;
            else if (gameobject.GetComponent<Image>())
                gameobject.GetComponent<Image>().color = _colorDifficulty;
        }
    }
    public void ColorShadowsButtons(GameObject UI)
    {
        var allShadows = UI.GetComponentsInChildren<Shadow>(true);
        var difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        foreach (var shadow in allShadows)
        {
            shadow.effectColor = GetDifficultyColor();
        }
    }
}
