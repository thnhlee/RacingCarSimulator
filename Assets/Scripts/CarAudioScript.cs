using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarAudioScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CarController carController; 
    private AudioManager audioManager;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip engineClip;
    private AudioSource engineAudioSource;

    [Header("Speed Audio Mapping")]
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.5f;


    private void Awake()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (audioManager == null)
        {
            audioManager = AudioManager.Instance;
            if (audioManager == null)
            {
                Debug.LogError("AudioManager instance not found!");
                enabled = false;
                return;
            }
        }

        if (carController == null)
        {
            carController = FindObjectOfType<CarController>();
            if (carController == null)
            {
                Debug.LogError("CarController not found in the scene!");
                enabled = false;
                return;
            }
            else
            {
                Debug.Log("CarController successfully found.");
            }
        }
        else
        {
            Debug.Log("CarController already assigned via Inspector.");
        }

        if (engineAudioSource == null)
        {
            engineAudioSource = GetComponent<AudioSource>();
            if (engineAudioSource == null)
            {
                Debug.LogError("AudioSource component missing from the car!");
                enabled = false;
                return;
            }
            else
            {
                if (engineClip != null)
                {
                    engineAudioSource.clip = engineClip;
                }
                else
                {
                    Debug.LogError("EngineClip is not assigned!");
                }

                engineAudioSource.loop = true;
                engineAudioSource.playOnAwake = false;
                engineAudioSource.volume = 0.5f;
            }
        }
    }

    private void Start()
    {
        if (engineAudioSource != null && engineClip != null)
        {
            if (!engineAudioSource.isPlaying)
            {
                engineAudioSource.Play();
                Debug.Log("Engine sound started playing on loop.");
            }
        }
        else
        {
            Debug.LogWarning("EngineAudioSource or EngineClip is not properly assigned.");
        }
    }

    private void Update()
    {
        if (carController == null || audioManager == null || engineAudioSource == null || engineClip == null)
        {
            if (carController == null)
                Debug.LogWarning("CarController reference is missing in CarAudioScript.");

            if (audioManager == null)
                Debug.LogWarning("AudioManager reference is missing in CarAudioScript.");

            if (engineAudioSource == null)
                Debug.LogWarning("Engine AudioSource is not initialized in CarAudioScript.");

            if (engineClip == null)
                Debug.LogWarning("EngineClip is not assigned in CarAudioScript.");

            return; 
        }

        float speed = carController.GetCurrentSpeed();

        float maxSpeed = carController.GetMaxSpeed();
        if (maxSpeed <= 0f)
        {
            Debug.LogWarning("MaxSpeed is zero. Cannot normalize speed.");
            return;
        }

        float normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);

        float targetPitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
        engineAudioSource.pitch = targetPitch;

        float targetVolume = Mathf.Lerp(0.3f, 1f, normalizedSpeed);
        engineAudioSource.volume = targetVolume;
    }
}
