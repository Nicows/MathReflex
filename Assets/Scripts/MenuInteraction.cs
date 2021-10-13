using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    
    public void StartGame(){
        SceneManager.LoadScene(1); //1 for MainScene (the run)
    }

}