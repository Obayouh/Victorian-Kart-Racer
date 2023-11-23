using System.Collections.Generic;
using UnityEngine;

public static class LaptimeManagerStatic
{
    private const int MaxTopTimes = 10;
    private static List<float> topTimes = new List<float>();

    public static void SaveLapTime(float lapTime)
    {
        topTimes.Add(lapTime);
        topTimes.Sort();

        // Keep only the top MaxTopTimes lap times
        if (topTimes.Count > MaxTopTimes)
        {
            topTimes.RemoveAt(0);
        }

        SaveTopTimes();
    }

    public static List<float> LoadTopTimes()
    {
        topTimes.Clear();

        for (int i = 1; i <= MaxTopTimes; i++)
        {
            float lapTime = PlayerPrefs.GetFloat("TopTime" + i, 0f);
            if (lapTime > 0f)
            {
                topTimes.Add(lapTime);
            }
        }

        return topTimes;
    }

    private static void SaveTopTimes()
    {
        for (int i = 1; i <= topTimes.Count; i++)
        {
            PlayerPrefs.SetFloat("TopTime" + i, topTimes[i - 1]);
        }
    }
}