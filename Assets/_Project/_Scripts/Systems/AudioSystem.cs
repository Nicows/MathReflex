using UnityEngine;

/// <summary>
/// Insanely basic audio system which supports 3D sound.
/// Ensure you change the 'Sounds' audio source to use 3D spatial blend if you intend to use 3D sounds.
/// </summary>
public class AudioSystem : PersistentSingleton<AudioSystem>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    [SerializeField] private AudioClip _musiqueClip;

    private void Start() {
        _musicSource = GetComponents<AudioSource>()[0];
        _soundsSource = GetComponents<AudioSource>()[1];
    }
    public void PlayerDefaultMusic()
    {
        _musicSource.clip = _musiqueClip;
        _musicSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _musicSource.clip = null;
    }


    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1, bool randomPitch = false)
    {
        _soundsSource.pitch = randomPitch ? Random.Range(0.9f, 1.1f) : 1f;
        _soundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }

    public void PlaySound(AudioClip clip, float vol = 1)
    {
        _soundsSource.PlayOneShot(clip, vol);
    }

    public void SetMusicByDifficulty()
    {
        var difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        var clip =  Resources.Load<AudioClip>(path: "Sounds/Mix1_" + difficulty);
        PlayMusic(clip);
    }
}