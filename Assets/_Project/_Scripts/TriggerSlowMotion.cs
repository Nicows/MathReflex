using UnityEngine;
using System;


public class TriggerSlowMotion : MonoBehaviour
{    
    public static event Action OnTriggerSlowMotion;

    private void OnTriggerEnter2D(Collider2D other) {
        OnTriggerSlowMotion?.Invoke();
        gameObject.SetActive(false);
    }
}
