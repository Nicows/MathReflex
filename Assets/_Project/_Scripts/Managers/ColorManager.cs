using UnityEngine;
using UnityEngine.UI;

public class ColorManager : StaticInstance<ColorManager>
{
    private Color _colorDifficulty;

    private void Start()
    {
        GetDifficultyColor();
        RefreshColorDifficultyInLevelComponents();
    }
    public Color GetDifficultyColor()
    {
        var currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        _colorDifficulty = GetColor(currentDifficulty);
        return _colorDifficulty;
    }
    public Color GetColor(string colorString)
    {
        var colorHex = ColorFromDifficulty(colorString);
        ColorUtility.TryParseHtmlString(colorHex, out var color);
        return color;
    }
    private string ColorFromDifficulty(string difficulty) => difficulty switch
    {
        "Easy" => "#6EC6BA",
        "Normal" => "#55C5FA",
        "Hard" => "#DD3829",
        "EasyCompleted" => "#47827A",
        "NormalCompleted" => "#3F8EB3",
        "HardCompleted" => "#A42318",
        _ => "#6EC6BA"
    };
    public void RefreshColorDifficultyInLevelComponents()
    {
        var gmsNeedColor = GameObject.FindGameObjectsWithTag("Color");
        
        foreach (var shadow in gmsNeedColor)
        {
            if(shadow.TryGetComponent<SpriteRenderer>(out var spriteRenderer)){
                spriteRenderer.color = _colorDifficulty;
            }
            if(shadow.TryGetComponent<Image>(out var image)){
                image.color = _colorDifficulty;
            }
            if(shadow.TryGetComponent<Shadow>(out var shadowComponent)){
                shadowComponent.effectColor = _colorDifficulty;
            }
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
