using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public TimeManager timeManager;
    
    public float moveSpeed = 10f;
    public bool canMove = false;
    public float maxSpeed = 20f;
    public bool hasChosen = false;

    Vector2 mousePos;

    public GameObject gameOver;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        Translate();
    }
    private void Translate(){
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    private void checkInput(){
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            timeManager.StartSlowmotion();
        }

        if(rb.velocity.x < maxSpeed) rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed , rb.velocity.y) ;      
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.collider.gameObject.tag == "Porte"){
            CameraShake.Instance.ShakeCamera(3f, 0.2f);
            gameOver.SetActive(true);
            Destroy(this);
            
        }
    }
}
