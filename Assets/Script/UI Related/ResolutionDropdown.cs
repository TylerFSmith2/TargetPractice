using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    public void ChangeResolution()
    {
        string[] splitArray = resolutionDropdown.options[resolutionDropdown.value].text.Split(char.Parse(" "));
        Screen.SetResolution(int.Parse(splitArray[0]), int.Parse(splitArray[2]), Screen.fullScreen);
    }
}
