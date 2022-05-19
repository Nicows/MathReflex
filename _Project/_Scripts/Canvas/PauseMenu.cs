using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonPause;
    [SerializeField] private GameObject _pauseMenuUi;

    private void OnEnable() {
        // MultipleGenerator.OnButtonPauseInterractable += IsButtonPauseInterractable;
        MultipleGenerator.OnIsAnswerCorrect += IsButtonPauseInterractable;
        MultipleGenerator.OnDisplayResults += DisableButtonPause;
    }
    private void OnDisable() {
        // MultipleGenerator.OnButtonPauseInterractable -= IsButtonPauseInterractable;
        MultipleGenerator.OnIsAnswerCorrect -= IsButtonPauseInterractable;
        MultipleGenerator.OnDisplayResults -= DisableButtonPause;
    }
    private void Start() {
        SetShadowInButtonPause();
    }
    private void SetShadowInButtonPause()
    {
        if(_buttonPause.TryGetComponent<Shadow>(out var shadow)) 
            shadow.effectColor = ColorManager.Instance.GetDifficultyColor();
    }

    private void IsButtonPauseInterractable(bool isInterractable) => _buttonPause.interactable = isInterractable;
    private void DisableButtonPause() => _buttonPause.interactable = false;

    public void Resume(){
        _pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause(){
        _pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Menu() => ReturnTo.Return("Menu");
    public void Quit() => Application.Quit();

}
