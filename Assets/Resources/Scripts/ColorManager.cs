using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static Color colorDifficulty;
    private void Start()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        colorDifficulty = GetColor(currentDifficulty);
        RefreshColor();
    }

    public static Color GetColor(string colorString)
    {
        Color color;
        string colorHex;

        switch (colorString)
        {
            case "green":
                colorHex = "#6EC6BA";
                break;
            case "blue":
                colorHex = "#55C5FA";
                break;
            case "red":
                colorHex = "#DD3829";
                break;
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

        ColorUtility.TryParseHtmlString(colorHex, out color);
        return color;
    }
    public static void RefreshColor()
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
}
