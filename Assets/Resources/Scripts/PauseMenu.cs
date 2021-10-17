using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;
    

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape)){
        //     Debug.Log("pressed");
        //     if(GameIsPaused){
        //         Resume();
        //     }else{
        //         Pause();
        //     }
        // }
    }
    public void Resume(){
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause(){
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
