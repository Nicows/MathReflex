using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Scripts")]
    public GameOver gameOver;
    public EndLevel endLevel;

    [Header("Components")]
    public Rigidbody2D rb;
    public Camera cam;
    public GameObject avatarUsed;

    [Header("Movements")]
    private float moveSpeed = 40f;
    public static bool isDead = false;

    private void Start()
    {
        GetPlayerAvatar();
        GetStatsFromDifficulty();
    }

    private void GetStatsFromDifficulty()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        switch (currentDifficulty)
        {
            case "Easy":
                moveSpeed = 30f;
                break;

            case "Normal":
                moveSpeed = 42f;
                break;

            case "Hard":
                moveSpeed = 50f;
                break;
            default: break;
        }
    }
    private void GetPlayerAvatar()
    {
        string avatarUsedPref = PlayerPrefs.GetString("AvatarUsed", "carre");
        avatarUsed.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path: "Images/Reflexion/" + avatarUsedPref);
    }

    private void Update()
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

        // if (rb.velocity.x < maxSpeed) rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Porte") PlayerDied();
    }
    private void PlayerDied()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        GetComponent<AudioSource>().Play();
        isDead = true;
        gameOver.StartGameOver();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(-moveSpeed, moveSpeed), ForceMode2D.Impulse);
        rb.AddTorque(30f, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EndLevel")
        {
            PlayerFinishedLevel(other.gameObject);
        }
        if (other.gameObject.tag == "Portal")
        {

            other.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponent<AudioSource>().Play();
            moveSpeed = 5f;
        }
    }
    private void PlayerFinishedLevel(GameObject other)
    {
        // other.GetComponent<AudioSource>().Play();
        moveSpeed = 1f;
        endLevel.FinishedLevel();
    }
}
