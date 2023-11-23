using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptimeManager : MonoBehaviour
{
    public Text topTimesText;
    public Laptime laptimeScript;
    private GameManager gameManagerScript;

    private void Start()
    {
        if (laptimeScript == null)
        {
            Debug.LogError("Laptime script not assigned to LaptimeManager.");
            return;
        }

        List<float> topTimes = LaptimeManagerStatic.LoadTopTimes();
        UpdateTopTimesUI(topTimes);
    }

    public void SaveLapTime()
    {
        float newLapTime = laptimeScript.GetElapsedTime();
        LaptimeManagerStatic.SaveLapTime(newLapTime);
        List<float> topTimes = LaptimeManagerStatic.LoadTopTimes();
        UpdateTopTimesUI(topTimes);
    }

    public void UpdateTopTimesUI(List<float> topTimes)
    {
        topTimes.Sort();
        string topTimesString = "";

        for (int i = 0; i < topTimes.Count; i++)
        {
            int rank = i + 1;
            topTimesString += $"{rank}. {FormatTime(topTimes[i])}\n";
        }

        topTimesText.text = topTimesString;
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 100) % 100);

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void SetGameManager(GameManager manager)
    {
        gameManagerScript = manager;
    }
}