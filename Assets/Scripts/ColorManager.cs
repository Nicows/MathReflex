using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static Color colorDifficulty;
    private void Start()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Facile":
            case "Easy":
                colorDifficulty = GetColor("green");
                break;

            case "Normal":
                colorDifficulty = GetColor("blue");
                break;

            case "Difficile":
            case "Hard":
                colorDifficulty = GetColor("red");
                break;

            default:
                colorDifficulty = GetColor("green");
                break;
        }

        RefreshColor();
    }

    public static Color GetColor(string colorString)
    {
        Color color;
        string colorHex;

        switch (colorString)
        {
            case "green":
                colorHex = "#54FBC8";
                break;
            case "blue":
                colorHex = "#55C5FA";
                break;
            case "red":
                colorHex = "#EF3829";
                break;
            case "levelFinished":
                colorHex = "#008080";
                break;
            default:
                colorHex = "#54FBC8";
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
