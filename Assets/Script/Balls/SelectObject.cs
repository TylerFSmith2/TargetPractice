using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SelectObject : MonoBehaviour
{
    public LayerMask ballMask;
    public IntVar ballBounceThrustSO, numClicksSO, numRedLineClicksSO;

    public BoolVar deleteBallSO, timePauseSO;


    public RemoveBall removeBall;

    private Transform _lastSelection;

    public Texture2D crosshair;

    public AudioSource sniperSoundSource;
    public AudioClip sniperShot;

    //For setting achievements
    public SteamAchievements steamAchieve;

    // Start is called before the first frame update
    void Start()
    {
        deleteBallSO.value = false;
        //TODO playerprefs?
        if (!PlayerPrefs.HasKey("PlayerPrefsNumberClicks"))
        {
            PlayerPrefs.SetInt("PlayerPrefsNumberClicks", 0);
            PlayerPrefs.SetInt("PlayerPrefsNumberRedClicks", 0);
        }

        numClicksSO.value = PlayerPrefs.GetInt("PlayerPrefsNumberClicks");
        numRedLineClicksSO.value = PlayerPrefs.GetInt("PlayerPrefsNumberRedClicks");
    }
    
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ballMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                sniperSoundSource.PlayOneShot(sniperShot);
                //Increment score multiplier every bounce
                if (deleteBallSO.value)
                {
                    hit.transform.GetComponent<Ball>().BallDestruction();
                    deleteBallSO.value = false;
                    removeBall.barCD.ResetCD(removeBall.barCDMax.value);
                    Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
                    Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
                }
                else
                {
                    //Bounce Target
                    hit.transform.GetComponent<Ball>().BallBounce(ballBounceThrustSO.value, true);


                    UnlockTimePauseBounce();

                    //If add to the overall clicks. If below red line, add to that too
                    numClicksSO.value++;

                    //Test num clicks for if it should get an achievement
                    steamAchieve.TestTripleAchieve(numClicksSO.value, new string[] { "Click A Lot 1", "Click A Lot 2", "Click A Lot 3" }, new int[] { 1000, 10000, 100000});


                    if (hit.transform.GetComponent<Ball>().testBelowLowScoreLine())
                    {
                        numRedLineClicksSO.value++;
                        steamAchieve.TestTripleAchieve(numRedLineClicksSO.value, new string[] { "Savior 1", "Savior 2", "Savior 3" }, new int[] { 100, 1000, 10000 });
                        //Test num clicks for if it should get an achievement
                        //TestAchieve(new string[] { "Click A Lot 1", "Click A Lot 2", "Click A Lot 3" }, new int[] { 1000, 10000, 100000 });
                    }
                }
            }
            _lastSelection = hit.transform;
        }
    }

    private void UnlockTimePauseBounce()
    {
        bool unlocked;
        SteamUserStats.GetAchievement("Time Pause Bounce", out unlocked);
        if (!unlocked && timePauseSO.value)
        {
            steamAchieve.UnlockSteamAchievement("Time Pause Bounce");
        }
    }
}
