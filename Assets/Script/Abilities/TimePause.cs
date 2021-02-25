using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePause : MonoBehaviour
{
    // Start is called before the first frame update

    //SO to be used for declaring time is paused
    public BoolVar timePauseSO;

    //Text fields
    public GameObject durationText; //Gameobject so it can be set active/inactive
    public Text timePauseText;

    //Numbers for the duration of time pause
    private float _myDur;
    private int countdown;

    //Number for the cooldown of time pause
    private UIBarControl barCD;
    public IntVar barCDMax;

    void Start()
    {
        barCD = GetComponent<UIBarControl>();
        timePauseSO.value = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TimePauseActivated(5);
        }
        if (_myDur >= 0 && timePauseSO.value) //Time pause countdown ongoing
        {
            if (_myDur < countdown)
            {
                durationText.GetComponent<Text>().text = "Time Left: " + countdown;
                barCD.fill.color = new Color(0f, 0.55f, 0f);
                countdown -= 1;
            }
            _myDur -= Time.deltaTime;
            timePauseText.text = "Q: Cancel";
        }
        else if(timePauseSO.value) //Time pause ends (Dur is 0)
        {
            timePauseSO.value = false;
            timePauseText.text = "Q: Time Pause";
            barCD.ResetCD(barCDMax.value);
            durationText.SetActive(!durationText.activeInHierarchy);
        }
    }

    public void TimePauseActivated(float duration)
    {
        if(_myDur > 0)
        {
            _myDur = 0;
        }
        else if (barCD.cooldownBar <= 1.0f)
        {
            timePauseSO.value = true;
            _myDur = duration;
            countdown = (int)_myDur;
            durationText.SetActive(!durationText.activeInHierarchy);
        }        
    }
}
