using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class ScoreHandling : MonoBehaviour
{

    //Score Modifiers
    public IntVar scoreSO;
    public IntVar scoreMultiplierSO;
    public IntVar timeSO;

    //Text Fields
    public Text scoreText;

    //Escape Menu
    public GameObject escapeMenu;

    //For setting achievements
    public SteamAchievements steamAchieve;


    // Start is called before the first frame update
    void Start()
    {
        scoreSO.value = 0;
        scoreMultiplierSO.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
    }

    public void AddScore(int score)
    {
        bool unlocked = false;

        scoreSO.value += score;
        float ShownVal;
        //Above 1 billion
        if(scoreSO.value > 1000000000 || scoreSO.value < 0)
        {
            scoreText.text = "OVERFLOW";

            //Test if One Billion is unlocked
            SteamUserStats.GetAchievement("One Billion", out unlocked);
            if (!unlocked)
            {
                //If it is not unlocked, unlock it
                steamAchieve.UnlockSteamAchievement("One Billion");
            }
        }
        //Above 1 milion
        else if(scoreSO.value > 1000000)
        {
            ShownVal = (float)scoreSO.value / 1000000;
            scoreText.text = "Score: " + ShownVal.ToString("F2") + "m";

            //Test if One Million is unlocked
            SteamUserStats.GetAchievement("One Million", out unlocked);
            if (!unlocked)
            {
                //If it is not unlocked, unlock it
                steamAchieve.UnlockSteamAchievement("One Million");
            }

            //Bool to test if achievements are unlocked
            
            //Check if 25 Targets is unlocked
            
        }
        else
        {
            scoreText.text = "Score: " + scoreSO.value;
        }
    }
}
