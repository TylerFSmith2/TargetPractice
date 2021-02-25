using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class ChangeGameVariables : MonoBehaviour
{
    //Only change if its the first time around

    public GameObject MainMenu;

    public InputField MaxBallField, BallSpawnTimeriField, TimePauseTimerField, BounceAllTimeriField, RemoveBallTimeriField, DragiField, BallThrustiField;

    public IntVar MaxBallSO, BallSpawnTimerSO, TimePauseTimerSO, BounceAllTimerSO, RemoveBallTimerSO, DragSO, BallThrustSO;

    public Text MaxBallText, BallSpawnTimerText, TimePauseTimerText, BounceAllTimerText, RemoveBallTimerText, DragText, BallThrustText;

    //For setting achievements
    public SteamAchievements steamAchieve;

    public void BackToMain()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ChangeSO(IntVar SOtoChange)
    {
        bool success;
        int val = 0;

        if(SOtoChange == MaxBallSO)
        {
            if(MaxBallField.text == "")
            {
                return;
            }
            success = int.TryParse(MaxBallField.text, out val);
            SOtoChange.value = val;
            MaxBallField.text = null;
            MaxBallText.text = "Max Targets:\n" + val;
        }

        else if(SOtoChange == BallSpawnTimerSO)
        {
            if (BallSpawnTimeriField.text == "")
            {
                return;
            }
            success = int.TryParse(BallSpawnTimeriField.text, out val);
            if (val <= 1)
            {
                return;
            }
            SOtoChange.value = val;
            BallSpawnTimeriField.text = null;
            BallSpawnTimerText.text = "Target Spawn CD:\n" + val;
        }

        else if (SOtoChange == TimePauseTimerSO)
        {
            if (TimePauseTimerField.text == "")
            {
                return;
            }
            success = int.TryParse(TimePauseTimerField.text, out val);
            if (val <= 1)
            {
                return;
            }
            SOtoChange.value = val;
            TimePauseTimerField.text = null;
            TimePauseTimerText.text = "Time Pause CD:\n" + val;
        }

        else if (SOtoChange == BounceAllTimerSO)
        {
            if (BounceAllTimeriField.text == "")
            {
                return;
            }
            success = int.TryParse(BounceAllTimeriField.text, out val);
            if (val <= 1)
            {
                return;
            }
            SOtoChange.value = val;
            BounceAllTimeriField.text = null;
            BounceAllTimerText.text = "Bounce All CD:\n" + val;
        }

        else if (SOtoChange == RemoveBallTimerSO)
        {
            if (RemoveBallTimeriField.text == "")
            {
                return;
            }
            success = int.TryParse(RemoveBallTimeriField.text, out val);
            if (val <= 1)
            {
                return;
            }
            SOtoChange.value = val;
            RemoveBallTimeriField.text = null;
            RemoveBallTimerText.text = "Remove Target CD:\n" + val;
        }




        else if (SOtoChange == BallThrustSO)
        {
            if (BallThrustiField.text == "")
            {
                return;
            }
            success = int.TryParse(BallThrustiField.text, out val);
            SOtoChange.value = val;
            BallThrustiField.text = null;
            BallThrustText.text = "Target Thrust:\n" + val;
        }
        
        else if (SOtoChange == DragSO)
        {
            if (DragiField.text == "")
            {
                return;
            }
            success = int.TryParse(DragiField.text, out val);
            SOtoChange.value = val;
            DragiField.text = null;
            DragText.text = "Air Resistance:\n" + val;
        }

        if (val > 1000)
        {
            steamAchieve.UnlockSteamAchievement("Variable High");
        }
        else if (val < 0)
        {
            steamAchieve.UnlockSteamAchievement("Variable Negative");
        }
    }
}
