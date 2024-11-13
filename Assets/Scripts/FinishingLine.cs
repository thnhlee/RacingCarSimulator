using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingLine : MonoBehaviour
{
    public LapManager lapManager;

    void Start()
    {
        lapManager = FindObjectOfType<LapManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lapManager.TryCompleteLap();
        }
    }
}
