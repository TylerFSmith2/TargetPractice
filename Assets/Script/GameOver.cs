using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

public class GameOver : MonoBehaviour
{ 
    //Var to tell if the game is over or not
    public BoolVar gameOverVar;

    public IntVar scoreVar;
    public IntVar ballsBouncedVar;
    public IntVar maxBallsVar;
    public IntVar timeVar;

    public IntVar MaxBallSO, BallSpawnTimerSO, TimePauseTimerSO, BounceAllTimerSO, RemoveBallTimerSO, DragSO, BallThrustSO;

    public Spawner Spawner;

    //Menus to remove
    public GameObject escapeMenu;

    //Menu to reopen
    public MainMenu mainMenu;

    //Menu to open
    public GameObject gameOverMenu;

    //Text fields to update
    public Text scoreText;
    public Text ballsBouncedText;
    public Text maxBalls;
    public Text timeText;

    //For setting achievements
    public SteamAchievements steamAchieve;

    // Start is called before the first frame update
    void Start()
    {
        gameOverVar.value = false;
        Spawner = FindObjectOfType<Spawner>();
    }

    public void GameOverEvent()
    {
        //Test if stats line up with one of the achievements
        if(gameOverVar.value)
        {
            return;
        }
        TestMode();

        WaitTime(5);
        gameOverVar.value = true;
        Time.timeScale = 0;
        Spawner.RemoveAll();
        gameOverMenu.SetActive(true);

        escapeMenu.SetActive(false);

        scoreText.text = "Score: " + scoreVar.value;
        ballsBouncedText.text = "Target Bounced: " + ballsBouncedVar.value;
        maxBalls.text = "Max Target Active: " + (maxBallsVar.value + 1);
        timeText.text = "Time Alive: " + timeVar.value;
    }

    public IEnumerator WaitTime(int wait)
    {
        yield return new WaitForSeconds(wait);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void TestMode()
    {
        //If 5 minutes have elapsed
        if(timeVar.value <= 10)
        {
            return;
        }


        //Test super mega hard mode
        if (BallThrustSO.value <= 300 && DragSO.value <= 1)
        {
            //Unlock super mega hard mode
            steamAchieve.UnlockSteamAchievement("Super Mega Hard Mode");
            steamAchieve.UnlockSteamAchievement("Super Hard Mode");
            steamAchieve.UnlockSteamAchievement("Hard Mode");

        }
        //Test super hard mode
        else if (BallThrustSO.value <= 500 && DragSO.value <= 2)
        {
            //Unlock super hard mode
            steamAchieve.UnlockSteamAchievement("Super Hard Mode");
            steamAchieve.UnlockSteamAchievement("Hard Mode");

        }
        //Test hard mode
        else if (BallThrustSO.value <= 700 && DragSO.value <= 4)
        {
            //Unlock hard mode
            steamAchieve.UnlockSteamAchievement("Hard Mode");

        }


        //Test super mega easy mode
        if (BounceAllTimerSO.value < 5 && TimePauseTimerSO.value < 5 && RemoveBallTimerSO.value < 5)
        {
            //Unlock super mega Easy mode
            steamAchieve.UnlockSteamAchievement("Super Mega Easy Mode");
            steamAchieve.UnlockSteamAchievement("Super Easy Mode");
            steamAchieve.UnlockSteamAchievement("Easy Mode");

        }
        //Test super easy mode
        else if ((BounceAllTimerSO.value < 5 && TimePauseTimerSO.value < 5) || (BounceAllTimerSO.value < 5 && RemoveBallTimerSO.value < 5) || (TimePauseTimerSO.value < 5 && RemoveBallTimerSO.value < 5))
        {
            //Unlock super Easy mode
            steamAchieve.UnlockSteamAchievement("Super Easy Mode");
            steamAchieve.UnlockSteamAchievement("Easy Mode");

        }
        //Test easy mode
        else if (BounceAllTimerSO.value < 5 || TimePauseTimerSO.value < 5 || RemoveBallTimerSO.value < 5)
        {
            //Unlock Easy mode
            steamAchieve.UnlockSteamAchievement("Easy Mode");
        }
    }
}
