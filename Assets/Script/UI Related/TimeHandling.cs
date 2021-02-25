using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class TimeHandling : MonoBehaviour
{
    // Time Handling controls the Time, displaying how long the player has been playing
    public float timeDisplay = 0;
    public Text timeDisplayText;
    public IntVar timeSO;
    public BoolVar gameActiveVar;

    //For setting achievements
    public SteamAchievements steamAchieve;

    void Start()
    {
        gameActiveVar.value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActiveVar.value) { return; }
        timeDisplay += Time.deltaTime;
        timeDisplayText.text = "Time: " + Mathf.Floor(timeDisplay);
        timeSO.value = (int)timeDisplay;

        TestAchieve(new string[] { "5 Minutes", "10 Minutes", "20 Minutes" }, new int[] { 300, 600, 1200});
    }

    private void TestAchieve(string[] IDs, int[] numsToPass)
    {
        //Check if all Target achievements are obtained
        bool dontLoop = false;
        SteamUserStats.GetAchievement(IDs[2], out dontLoop);

        //Bool to test if achievements are unlocked
        bool unlocked = false;
        //Check if 25 Targets is unlocked
        SteamUserStats.GetAchievement(IDs[0], out unlocked);

        if (!dontLoop)
        {
            if (timeSO.value >= numsToPass[0] && !unlocked)
            {
                //Unlock Achievement
                steamAchieve.UnlockSteamAchievement(IDs[0]);
            }
            else if (timeSO.value <= numsToPass[0])
            {

            }
            else
            {
                SteamUserStats.GetAchievement(IDs[1], out unlocked);

                if (timeSO.value >= numsToPass[1] && !unlocked)
                {
                    steamAchieve.UnlockSteamAchievement(IDs[1]);
                }
                else if (timeSO.value <= numsToPass[1])
                {

                }
                else
                {
                    SteamUserStats.GetAchievement(IDs[2], out unlocked);

                    if (timeSO.value >= numsToPass[2] && !unlocked)
                    {
                        steamAchieve.UnlockSteamAchievement(IDs[2]);
                    }
                }
            }
        }
    }
}
