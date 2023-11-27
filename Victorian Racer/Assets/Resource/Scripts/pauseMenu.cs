using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private bool IsPaused = false;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    private void Start()
    {
        pausemenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (IsPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0.0f;
        IsPaused = true;
    }

    public void resumeGame()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    public void backToMain()
    {
        SceneManager.LoadScene(0);
        IsPaused = false;
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
