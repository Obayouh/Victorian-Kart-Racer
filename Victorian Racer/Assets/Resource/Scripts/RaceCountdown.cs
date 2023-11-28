using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCountdown : MonoBehaviour
{
    [SerializeField] private Text countdownText;
    [SerializeField] private Transform player;
    [SerializeField] private Laptime lapTimer;

    private bool startGame = true;

    private void Start()
    {
        //Freeze player/environment
        SetStatic(player, true);
        GetComponent<CarControls>().enabled = false;
        countdownText.enabled = false;
    }

    void SetStatic(Transform obj, bool isStatic) //pakt transform van het object en een bool of hij op static staat
    {
        //base object wordt op static gezet wordt aan geroepen en op static gezet 
        obj.gameObject.isStatic = isStatic;
        //nu pakt hij de children en zet het die ook op static
        foreach (Transform child in obj)
        {
            SetStatic(child, isStatic);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) && startGame == true)
        {
            StartCoroutine(Countdown(3));
            startGame = false;
        }
    }

    private IEnumerator Countdown(int seconds)
    {
        countdownText.enabled = true;

        int count = seconds;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownText.text = "";

        //Countdown is finished, start the game
        StartGame();
    }

    private void StartGame()
    {
        SetStatic(player, false);
        GetComponent<CarControls>().enabled = true;
        lapTimer.StartTimer();
    }
}
