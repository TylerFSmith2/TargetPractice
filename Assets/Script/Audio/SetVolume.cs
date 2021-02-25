using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using E7.Introloop;

public class SetVolume : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetLevel(float sliderVal)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }

}
