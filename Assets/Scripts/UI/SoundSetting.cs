using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public static SoundSetting Instance;

    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // �����̴� �� �ҷ����� (������ �⺻�� 1.0f)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        // ����� �Ŵ������� ����
        AudioManager.instance.MusicVolume(bgmSlider.value);
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(bgmSlider.value);

        // �� ����
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);

        // �� ����
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
