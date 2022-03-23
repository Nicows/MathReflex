using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static Color colorDifficulty;

    private void Start()
    {
        GetDifficultyColor();
        RefreshColorDifficultyInAllComponents();
    }
    public static Color GetDifficultyColor()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        colorDifficulty = GetColor(currentDifficulty);
        return colorDifficulty;
    }
    public static Color GetColor(string colorString)
    {
        string colorHex;

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

        Color color;
        ColorUtility.TryParseHtmlString(colorHex, out color);
        return color;
    }
    public static void RefreshColorDifficultyInAllComponents()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Color");

        foreach (GameObject gameobject in gameObjects)
        {
            if (gameobject.GetComponent<SpriteRenderer>())
                gameobject.GetComponent<SpriteRenderer>().color = colorDifficulty;
            else if (gameobject.GetComponent<Image>())
                gameobject.GetComponent<Image>().color = colorDifficulty;
        }
    }
    public static void ColorShadowsButtons(GameObject UI)
    {
        Shadow[] allShadows = UI.GetComponentsInChildren<Shadow>(true);
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        for (int i = 0; i < allShadows.Length; i++)
        {
            allShadows[i].effectColor = colorDifficulty;
        }
    }
}
