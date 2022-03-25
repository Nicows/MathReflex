using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject avatarUsed;
    [SerializeField] private AudioClip _audioHurt;
    private Rigidbody2D _rb;

    [Header("Movements")]
    private float _moveSpeed = 40f;
    public static bool IsDead = false;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerWin;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GetPlayerAvatar();
        GetStatsFromDifficulty();
        IsDead = false;
    }
    private void GetStatsFromDifficulty()
    {
        string currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        switch (currentDifficulty)
        {
            case "Easy":
                _moveSpeed = 30f;
                break;
            case "Normal":
                _moveSpeed = 42f;
                break;
            case "Hard":
                _moveSpeed = 50f;
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
        if (!IsDead)
            Translate();
    }

    private void Translate()
    {
        _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Porte")) PlayerDied();
    }
    private void PlayerDied()
    {        
        OnPlayerDeath?.Invoke();
        IsDead = true;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.AddForce(new Vector2(-_moveSpeed, _moveSpeed), ForceMode2D.Impulse);
        _rb.AddTorque(30f, ForceMode2D.Impulse);
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
            _moveSpeed = 5f;
        }
    }
    private void PlayerFinishedLevel(GameObject other)
    {
        // other.GetComponent<AudioSource>().Play();
        _moveSpeed = 1f;
        OnPlayerWin?.Invoke();
    }
}
