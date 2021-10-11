using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlowMotion : MonoBehaviour
{
    public TimeManager timeManager;
    public MultipleGenerator multipleGenerator;
    
    private void Awake() {
        timeManager = GameObject.FindObjectOfType<TimeManager>();
        multipleGenerator = GameObject.FindObjectOfType<MultipleGenerator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        multipleGenerator.GetNumbers();
        timeManager.StartCountdown();
        timeManager.StartSlowmotion();
    }
}
