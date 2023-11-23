using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Text topTimesText;  

    private List<float> topTimes = new List<float>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadTopTimes(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "main menu")
        {
            topTimesText = GameObject.Find("TopTimesText").GetComponent<Text>();

            if (topTimesText == null)
            {
                Debug.LogError("Text component not found in the main menu scene.");
            }

            UpdateTopTimesUI();
        }
        else if (scene.name == "RaceScene")
        {
            topTimes.Clear();
        }
    }

    private void LoadTopTimes()
    {
        for (int i = 1; i <= 10; i++)
        {
            float time = PlayerPrefs.GetFloat("TopTime" + i, 5999.9f); 
            topTimes.Add(time);
        }

        // Sort the top times
        topTimes.Sort();
    }
    public void SaveLapTime(float lapTime)
    {
        topTimes.Add(lapTime);
        topTimes.Sort();
        topTimes.RemoveAt(topTimes.Count - 1);

        for (int i = 1; i <= topTimes.Count; i++)
        {
            PlayerPrefs.SetFloat("TopTime" + i, topTimes[i - 1]);
        }

        PlayerPrefs.Save(); 
        UpdateTopTimesUI();
    }

    private void UpdateTopTimesUI()
    {
        if (topTimesText != null)
        {
            string topTimesString = "Top Times:\n";

            for (int i = 0; i < topTimes.Count; i++)
            {
                int rank = i + 1;
                topTimesString += $"{rank}. {FormatTime(topTimes[i])}\n";
            }

            topTimesText.text = topTimesString;
        }
    }

    string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 100) % 100);

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}