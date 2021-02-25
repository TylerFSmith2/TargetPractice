using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;
using UnityEngine.Audio;

public class PlayIntroloopAudio : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public IntroloopAudio[] allIntroloopAudio;

    // Start is called before the first frame update
    void Start()
    {
        IntroloopPlayer.Instance.Play(allIntroloopAudio[0]);
        IntroloopPlayer.Instance.SetMixerGroup(mixer);
        allIntroloopAudio[0].Volume = 0.1f;
    }
}
