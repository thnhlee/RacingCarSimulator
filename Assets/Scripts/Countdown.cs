using UnityEngine;
using TMPro;
using System.Collections;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText; 
    private CarController carController;             
    private StopWatch stopWatch;                    

    private void Start()
    {
        carController = FindObjectOfType<CarController>();
        stopWatch = FindObjectOfType<StopWatch>(); 

        if (carController != null)
        {
            carController.enabled = false; 
        }

        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        int countdownTime = 3;

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString(); 
            yield return new WaitForSeconds(1f);         
            countdownTime--;
        }

        countdownText.text = "Go!";                        
        
        yield return new WaitForSeconds(1f);               
        
        countdownText.text = "";                          

        if (carController != null)
        {
            carController.enabled = true;  
        }

        if (stopWatch != null)
        {
            stopWatch.StartStopwatch(); 
        }
    }
}
