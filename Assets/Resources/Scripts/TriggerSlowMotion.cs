using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlowMotion : MonoBehaviour
{
    [Header ("Scripts")]
    public MultipleGenerator multipleGenerator;
    
    private void Awake() {
        multipleGenerator = GameObject.FindObjectOfType<MultipleGenerator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        multipleGenerator.GenerateNumbers();
        TimeManager.instance.StartSlowmotion();
        gameObject.SetActive(false);
    }
}
