using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public GameOver gameOver;

    public float moveSpeed = 10f;
    public bool canMove = false;
    public float maxSpeed = 20f;
    public bool hasChosen = false;

    public static bool isDead = false;

    void Start()
    {
        
    }
    void Update()
    {
        if (isDead == false)
        {
            // checkInput();
            Translate();
        }
    }
    
    private void Translate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TimeManager.instance.StartSlowmotion();
        }

        if (rb.velocity.x < maxSpeed) rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Porte")
        {
            gameOver.PlayerIsDead();
            isDead = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(new Vector2(-moveSpeed, moveSpeed), ForceMode2D.Impulse);
            rb.AddTorque(30f, ForceMode2D.Impulse);
        }
    }
}
