using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public static SoundSetting Instance;

    public Slider bgmSlider;
    public Slider sfxSlider;

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(bgmSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }
}
