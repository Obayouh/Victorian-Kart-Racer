using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public int totalLaps = 3; 
    public Text lapTimeText;
    public Text endRaceText;
    public float restartDelay = 3f;
    public float cooldownTime = 15f; 
    public Laptime lapTimeScript;

    private int currentLap = 0;
    private bool raceFinished = false;
    private bool canTriggerFinish = true;
    private float lastFinishTime;
    private float totalRaceTime = 0f;

    void Start()
    {
        lapTimeText.text = "Lap: 0 / " + totalLaps;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !raceFinished && canTriggerFinish)
        {
            if (currentLap < totalLaps)
            {
                float lapTime = lapTimeScript.GetElapsedTime();
                totalRaceTime += lapTime;
                lapTimeScript.ResetTimer();

                currentLap++;

                lapTimeText.text = "Lap: " + currentLap + " / " + totalLaps + "\nTime: " + FormatTime(totalRaceTime);

                if (currentLap == totalLaps)
                {
                    raceFinished = true;
                    endRaceText.text = "Race Finished!\nTotal Time: " + FormatTime(totalRaceTime);
                    Invoke("RestartGame", restartDelay);
                }

                canTriggerFinish = false;
                lastFinishTime = Time.time;
                Invoke("ResetCooldown", cooldownTime);
            }
        }
    }

    void ResetCooldown()
    {
        canTriggerFinish = true;
    }

    void RestartGame()
    {
        currentLap = 0;
        raceFinished = false;
        totalRaceTime = 0f;
        lapTimeText.text = "Lap: 0 / " + totalLaps;
        endRaceText.text = "";
        SceneManager.LoadScene(0);
    }

    string FormatTime(float time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}