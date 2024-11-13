using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    [Header("References")]
    public LapManager lapManager; 

    [Header("Progression Settings")]
    [Tooltip("Array of sprites representing each progress stage.")]
    [SerializeField] private Sprite[] progressSprites; 

    [Tooltip("Image component that displays the current progress sprite.")]
    [SerializeField] private Image progressImage; 

    private int totalCheckpoints;
    private int maxLap;
    private int currentCheckpoint = 0;
    private int maxCheckpoints;

    private int currentSpriteIndex = 0; 

    private void Start()
    {
        if (lapManager == null)
        {
            Debug.LogError("LapManager is not assigned in the PlayerProgress script.");
            return;
        }

        if (progressSprites == null || progressSprites.Length == 0)
        {
            Debug.LogError("Progress Sprites array is not assigned or empty in the PlayerProgress script.");
            return;
        }

        if (progressImage == null)
        {
            Debug.LogError("Progress Image is not assigned in the PlayerProgress script.");
            return;
        }

        if (progressSprites.Length != 6)
        {
            Debug.LogWarning($"Expected 6 progress sprites, but found {progressSprites.Length}. Ensure you have exactly 6 sprites.");
        }

        totalCheckpoints = lapManager.GetTotalCheckpoint();
        maxLap = lapManager.GetMaxLap();
        maxCheckpoints = totalCheckpoints * maxLap;
        currentCheckpoint = 0;
        currentSpriteIndex = 0;

        InitializeProgressImage();
    }

    private void InitializeProgressImage()
    {
        if (progressSprites.Length > 0)
        {
            progressImage.sprite = progressSprites[0];
        }
    }

    public void UpdateCheckpoint()
    {
        if (currentCheckpoint < maxCheckpoints)
        {
            currentCheckpoint++;
            UpdateProgressImage();
        }
        else
        {
            Debug.Log("Maximum checkpoints reached.");
        }
    }


    public void SetCheckPoint(int checkpoint)
    {
        currentCheckpoint = Mathf.Clamp(checkpoint, 0, maxCheckpoints);
        UpdateProgressImage();
    }


    public void SetMaxCheckPoint(int maxCheckpoints)
    {
        this.maxCheckpoints = maxCheckpoints;
        currentCheckpoint = 0;
        currentSpriteIndex = 0;
        InitializeProgressImage();
    }

    private void UpdateProgressImage()
    {
        if (progressSprites.Length == 0 || progressImage == null)
            return;

        float progressFraction = (float)currentCheckpoint / maxCheckpoints;

        int newSpriteIndex = Mathf.Clamp(Mathf.FloorToInt(progressFraction * progressSprites.Length), 0, progressSprites.Length - 1);

        if (newSpriteIndex != currentSpriteIndex)
        {
            progressImage.sprite = progressSprites[newSpriteIndex];
            currentSpriteIndex = newSpriteIndex;
        }
    }
}
