using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laptime : MonoBehaviour
{
    public Text timerText;
    private float elapsedTime;
    private bool isRunning = false;

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }


    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int hours = (int)(elapsedTime / 3600);
        int minutes = (int)((elapsedTime % 3600) / 60);
        int seconds = (int)(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

}