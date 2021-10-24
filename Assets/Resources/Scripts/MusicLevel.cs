using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevel : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        SetMusicByDifficulty();
        IsMusicEnabled();
    }

    private void SetMusicByDifficulty()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        audioSource.clip =  Resources.Load<AudioClip>(path: "Sounds/Mix1_" + difficulty);
        audioSource.Play();
    }
    private void IsMusicEnabled()
    {
        int isMusicEnabled = PlayerPrefs.GetInt("Music", 0);
        if (isMusicEnabled == 1) audioSource.volume = 0.3f;
        else audioSource.volume = 0f;
    }
}
