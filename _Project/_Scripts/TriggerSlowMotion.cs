using UnityEngine;

public class TriggerSlowMotion : MonoBehaviour
{    
    public static event System.Action OnTriggerSlowMotion;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) 
            OnTriggerSlowMotion?.Invoke();
    }
}
