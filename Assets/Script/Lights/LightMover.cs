using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMover : MonoBehaviour
{
    public bool AllLightsMove, AllLightsOn, AllLightsHaveColor;

    public Toggle SMovement, SActive, SColor, EMovement, EActive, EColor;

    [SerializeField]
    List<GameObject> LightList;

    [SerializeField]
    private GameObject BackWall;

    public void Start()
    {
        if(!PlayerPrefs.HasKey("PlayerPrefAllLightsMove"))
        {
            PlayerPrefs.SetInt("PlayerPrefAllLightsMove", 1);
            PlayerPrefs.SetInt("PlayerPrefAllLightsOn", 1);
            PlayerPrefs.SetInt("PlayerPrefAllLightsHasColor", 1);
        }
        //Set the bools to be the same as the player prefs
        AllLightsMove = (PlayerPrefs.GetInt("PlayerPrefAllLightsMove") == 1) ? true : false;
        AllLightsOn = (PlayerPrefs.GetInt("PlayerPrefAllLightsOn") == 1) ? true : false;
        AllLightsHaveColor = (PlayerPrefs.GetInt("PlayerPrefAllLightsHasColor") == 1) ? true : false;
        if (AllLightsHaveColor)
        {
            ResetLightColor();
        }
        else
        {
            TurnGreyscale();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!AllLightsMove)
        {
            return;
        }
        foreach (GameObject l in LightList)
        {
            if(l.transform.position.y > 20.0f)
            {
                //Reset position
                l.transform.position = transform.position;
            }
            else
            {
                //Get sideways movement
                bool direction = l.GetComponent<LightMovement>().GetDir();
                //Move up and sideways
                float randNumY = Random.Range(0.03f, 0.08f);
                float randNumX = Random.Range(0.02f, 0.05f);
                float randNumZ = Random.Range(0.02f, 0.05f);
                if (!direction)
                {
                    randNumX *= -1;
                    randNumZ *= -1;
                }
                l.transform.position += new Vector3(randNumX, randNumY, randNumZ);
            }  
        }
    }

    public void LightsMove()
    {
        AllLightsMove = !AllLightsMove;
        //Sets PlayerPrefAllLightsMove to 1 if AllLightsMove = true, or 0 if AllLightsMove = false
        PlayerPrefs.SetInt("PlayerPrefAllLightsMove", AllLightsMove ? 1 : 0);
        foreach (GameObject l in LightList)
        {
            l.GetComponent<LightMovement>().lightsMove = AllLightsMove;
        }
    }

    public void LightsOnOff()
    {
        AllLightsOn = !AllLightsOn;
        //Sets PlayerPrefAllLightsMove to 1 if AllLightsMove = true, or 0 if AllLightsMove = false
        PlayerPrefs.SetInt("PlayerPrefAllLightsOn", AllLightsOn ? 1 : 0);
        foreach (GameObject l in LightList)
        {
            l.SetActive(AllLightsOn);
        }
        LightsMove();
    }

    public void LightsColor()
    {
        if(AllLightsHaveColor)
        {
            TurnGreyscale();
        }
        else
        {
            ResetLightColor();
        }
        AllLightsHaveColor = !AllLightsHaveColor;
    }

    public void TurnGreyscale()
    {
        PlayerPrefs.SetInt("PlayerPrefAllLightsHasColor", 0);
        foreach (GameObject l in LightList)
        {
            l.GetComponent<LightMovement>().GreyscaleLight();
        }
    }

    public void ResetLightColor()
    {
        PlayerPrefs.SetInt("PlayerPrefAllLightsHasColor", 1);
        foreach (GameObject l in LightList)
        {
            l.GetComponent<LightMovement>().ResetColor();
        }
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("PlayerPrefAllLightsMove"))
        {
            PlayerPrefs.SetInt("PlayerPrefAllLightsMove", 1);
            PlayerPrefs.SetInt("PlayerPrefAllLightsOn", 1);
            PlayerPrefs.SetInt("PlayerPrefAllLightsHasColor", 1);
        }
        //Set the bools to be the same as the player prefs
        AllLightsMove = (PlayerPrefs.GetInt("PlayerPrefAllLightsMove") == 1) ? true : false;
        AllLightsOn = (PlayerPrefs.GetInt("PlayerPrefAllLightsOn") == 1) ? true : false;
        AllLightsHaveColor = (PlayerPrefs.GetInt("PlayerPrefAllLightsHasColor") == 1) ? true : false;

        if (AllLightsHaveColor)
        {
            ResetLightColor();
        }
        else
        {
            TurnGreyscale();
        }
    }
}
