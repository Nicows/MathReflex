using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonPause;
    [SerializeField] private GameObject _pauseMenuUi;

    private void OnEnable() {
        MultipleGenerator.OnEnableButtonPause += EnableButtonPause;
        MultipleGenerator.OnDisableButtonPause += DisableButtonPause;
    }
    private void OnDisable() {
        MultipleGenerator.OnEnableButtonPause -= EnableButtonPause;
        MultipleGenerator.OnDisableButtonPause -= DisableButtonPause;
    }
    private void Start() {
        if(_buttonPause.TryGetComponent<Shadow>(out var shadow)) {
            shadow.effectColor = ColorManager.Instance.GetDifficultyColor();
        }
    }

    private void DisableButtonPause() => _buttonPause.interactable = false;

    private void EnableButtonPause() => _buttonPause.interactable = true;

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
