using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void RestartGame(){
        PlayerBehaviour.isDead = false;
        TimeManager.instance.StopSlowmotion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
