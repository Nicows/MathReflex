using System;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject avatarUsed;
    [SerializeField] private AudioClip _audioHurt;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    [Header("Movements")]
    private float _moveSpeed = 40f;
    public static bool IsDead = false;
    private bool _hasStarted = false;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerWin;

    private void Start()
    {
        GetPlayerComponents();
        GetPlayerAvatar();
        GetStatsFromDifficulty();
        IsDead = false;
        _hasStarted = false;
    }

    private void GetPlayerComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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
    public void StartGame() => _hasStarted = true;

    private void Update()
    {
        if (!_hasStarted)
        {
            if (Input.touchCount > 0)
                _hasStarted = true;
            if (Input.GetMouseButtonDown(0))
                _hasStarted = true;

            return;
        }
        if (!IsDead && _hasStarted)
            Translate();
    }

    private void Translate()
    {
        _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
    }
    private void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
        IsDead = true;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.AddForce(new Vector2(-_moveSpeed, _moveSpeed), ForceMode2D.Impulse);
        _rb.AddTorque(30f, ForceMode2D.Impulse);
        _audioSource.PlayOneShot(_audioHurt);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Porte")) PlayerDied();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndLevel"))
        {
            PlayerFinishedLevel();
        }
        if (other.gameObject.CompareTag("Portal"))
        {
            other.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponent<AudioSource>().Play();
            _moveSpeed = 5f;
        }
    }
    private void PlayerFinishedLevel()
    {
        _moveSpeed = 1f;
        OnPlayerWin?.Invoke();
        StartCoroutine(WaitBeforeStopMovement(2f));
    }
    IEnumerator WaitBeforeStopMovement(float secondes)
    {
        yield return new WaitForSeconds(secondes);
        _moveSpeed = 0f;
    }
}
