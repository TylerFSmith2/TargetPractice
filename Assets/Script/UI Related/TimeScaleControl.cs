using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleControl : MonoBehaviour
{
    //Controls when game is paused

    //Vars set in EscMenu and CameraMovement
    public IntVar menuTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        //Start unpaused
        menuTimeScale.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //If both vars are 1, unpause
        if (menuTimeScale.value == 1)
        {
            Time.timeScale = 1;
        }
        //If either var is 0, pause
        else if (menuTimeScale.value == 0)
        {
            Time.timeScale = 0;
        }
    }
}
