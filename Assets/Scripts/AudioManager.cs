using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource preGameplayMusicSource;
    public AudioSource gameplayMusicSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            Debug.Log("AudioManager initialized and set to DontDestroyOnLoad.");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate AudioManager instance destroyed.");
        }
    }

    private void InitializeAudioSources()
    {
        if (preGameplayMusicSource == null || gameplayMusicSource == null)
        {
            Debug.LogError("Audio Sources are not assigned in the AudioManager.");
        }
        else
        {
            Debug.Log("Audio Sources successfully initialized.");
        }
    }

    public void PlayPreGameplayMusic()
    {
        if (preGameplayMusicSource != null)
        {
            preGameplayMusicSource.Play();
            Debug.Log("Playing Pre-Gameplay Music.");
        }
    }

    public void StopPreGameplayMusic()
    {
        if (preGameplayMusicSource != null)
        {
            preGameplayMusicSource.Stop();
            Debug.Log("Stopped Pre-Gameplay Music.");
        }
    }

    public void PlayGameplayMusic()
    {
        if (gameplayMusicSource != null)
        {
            gameplayMusicSource.Play();
            Debug.Log("Playing Gameplay Music.");
        }
    }

    public void StopGameplayMusic()
    {
        if (gameplayMusicSource != null)
        {
            gameplayMusicSource.Stop();
            Debug.Log("Stopped Gameplay Music.");
        }
    }
}
