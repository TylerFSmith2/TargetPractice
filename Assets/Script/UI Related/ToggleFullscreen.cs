using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFullscreen : MonoBehaviour
{
    public void SetFullScreen(bool fullScreenValue)
    {
        Screen.fullScreen = fullScreenValue;
        if (!fullScreenValue)
        {
            Resolution resolution = Screen.currentResolution;
            Screen.SetResolution(resolution.width, resolution.height, fullScreenValue);
        }
        PlayerPrefs.SetInt("PlayerPrefsFullscreen", fullScreenValue ? 1 : 0);
    }
}
