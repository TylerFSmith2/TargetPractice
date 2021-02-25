using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Steamworks;

public class MainMenu : MonoBehaviour
{
    public BoolVar gameActiveVar;
    public Spawner spawner;
    public GameObject ChangeVarMenu, SettingsMenu, EscapeMenu;

    public IntVar MaxBallSO, BallSpawnTimerSO, TimePauseTimerSO, BounceAllTimerSO, RemoveBallTimerSO, DragSO, BallThrustSO;

    public Text MaxBallText, BallSpawnTimerText, TimePauseTimerText, BounceAllTimerText, RemoveBallTimerText, DragText, BallThrustText;

    //For setting achievements
    public SteamAchievements steamAchieve;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerPrefsFullscreen"))
        {
            PlayerPrefs.SetInt("PlayerPrefsFullscreen", 1);
        }

        Screen.fullScreen = (PlayerPrefs.GetInt("PlayerPrefsFullscreen") == 1 ? true : false);

        MaxBallText.text = "Max Targets: \n" + MaxBallSO.value;
        BallSpawnTimerText.text = "Target Spawn CD: \n" + BallSpawnTimerSO.value;
        TimePauseTimerText.text = "Time Pause CD: \n" + TimePauseTimerSO.value;
        BounceAllTimerText.text = "Bounce All CD: \n" + BounceAllTimerSO.value;
        RemoveBallTimerText.text = "Remove Target CD: \n" + RemoveBallTimerSO.value;
        DragText.text = "Air Resistance: \n" + DragSO.value;
        BallThrustText.text = "Target Thrust: \n" + BallThrustSO.value;
    }

    public void PlayGame()
    {
        if(!gameActiveVar.value)
        {
            gameActiveVar.value = true;
            spawner.StartGame();
            gameObject.SetActive(false);
        }
    }

    public void OpenMenu(GameObject menu)
    {
        if(menu.gameObject.name.Equals("Credits"))
        {
            steamAchieve.UnlockSteamAchievement("Look At Credits");
        }
        menu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
