using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("started game");
    }

    public void Settings()
    {
        Debug.Log("settings");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void Laptimes()
    {
        SceneManager.LoadScene(3);
        Debug.Log("laptimes");
    }
}