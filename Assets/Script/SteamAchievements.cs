using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{
    public static SteamAchievements script;

    //if achieve is unlocked
    private bool unlockTest;

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            uint numAchievements = SteamUserStats.GetNumAchievements();
            for (uint i = 0; i < numAchievements; ++i)
            {
                string achName = SteamUserStats.GetAchievementName(i);
                SteamUserStats.ClearAchievement(achName);
                //Debug.Log(achName + " cleared?: " + SteamUserStats.ClearAchievement(achName));
            }
            
        }
    }*/

    private void Awake()
    {
        script = this;
        if (!SteamManager.Initialized)
        {
            gameObject.SetActive(false);
            return;
        }
        //Debug.Log("Awake");
        uint numAchievements = SteamUserStats.GetNumAchievements();
        for (uint i = 0; i < numAchievements; ++i)
        {
            string achName = SteamUserStats.GetAchievementName(i);
            //Debug.Log("Achievement name: " + achName);
        }
        //Debug.Log(SteamUserStats.SetAchievement());
    }
    
    //string ID is the API Name in steamworks
    public void UnlockSteamAchievement(string ID)
    {
        TestSteamAchievement(ID);
        if(!unlockTest)
        {
            SteamUserStats.SetAchievement(ID);
            SteamUserStats.StoreStats();
        }
        //Debug.Log("DONE");
    }

    void TestSteamAchievement(string ID)
    {
        SteamUserStats.GetAchievement(ID, out unlockTest);

        //Debug.Log(unlockTest);
    }

    public void TestTripleAchieve(int InttoTest, string[] IDs, int[] numsToPass)
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
            if (InttoTest >= numsToPass[0] && !unlocked)
            {
                //Unlock Achievement
                UnlockSteamAchievement(IDs[0]);
            }
            else if (InttoTest <= numsToPass[0])
            {

            }
            else
            {
                SteamUserStats.GetAchievement(IDs[1], out unlocked);
                if (InttoTest >= numsToPass[1] && !unlocked)
                {
                    UnlockSteamAchievement(IDs[1]);
                }
                else if (InttoTest <= numsToPass[1])
                {

                }
                else
                {
                    SteamUserStats.GetAchievement(IDs[2], out unlocked);
                    if (InttoTest >= numsToPass[2] && !unlocked)
                    {
                        UnlockSteamAchievement(IDs[2]);
                    }
                }
            }
        }
    }
}
