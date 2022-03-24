using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUi;
    public ReturnTo returnTo;
    
    public void Resume(){
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause(){
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Menu(){
        returnTo.Return("Menu");
    }
    public void Quit(){
        Application.Quit();
    }
    
}
