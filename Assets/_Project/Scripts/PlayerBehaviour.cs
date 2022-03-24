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
    public GameObject avatarUsed;

    [Header("Movements")]
    private float moveSpeed = 40f;
    public static bool isDead = false;
    [SerializeField] private AudioClip _audioHurt;

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
        }
    }
    private void GetPlayerAvatar()
    {
        var avatarUsedPref = PlayerPrefs.GetString("AvatarUsed", "carre");
        avatarUsed.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path: "Avatars/" + avatarUsedPref);
    }

    private void Update()
    {
        if (!isDead)
            Translate();
    }

    private void Translate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Porte")) PlayerDied();
    }
    private void PlayerDied()
    {        
        isDead = true;
        gameOver.StartGameOver();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(-moveSpeed, moveSpeed), ForceMode2D.Impulse);
        rb.AddTorque(30f, ForceMode2D.Impulse);
        AudioSystem.Instance.PlaySound(_audioHurt, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndLevel"))
        {
            PlayerFinishedLevel(other.gameObject);
        }
        if (other.gameObject.CompareTag("Portal"))
        {
            other.GetComponentInChildren<ParticleSystem>().Play();
            // other.GetComponent<AudioSource>().Play();
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
