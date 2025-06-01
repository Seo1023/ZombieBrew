using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("���� �� �̸�: " + sceneName);

        switch (sceneName)
        {
            case "LobbyScene":
                PlayMusic("MainMenuBGM");
                break;
            case "SelectScene":
                PlayMusic("MainMenuBGM");
                break;
            case "MainScene":
                PlayMusic("BGM");
                break;
            default:
                PlayMusic("BGM"); 
                break;
        }
    }*/

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        Debug.Log("���� �� �̸�: " + sceneName);

        switch (sceneName)
        {
            case "LobbyScene":
                PlayMusic("MainMenuBGM");
                break;
            case "SelectScene":
                PlayMusic("MainMenuBGM");
                break;
            case "MainScene":
                PlayMusic("MainGameBGM");
                break;
            case "LoadingScene":
                PlayMusic("MainMenuBGM");
                break;
            default:
                PlayMusic("BGM");
                break;
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("BGM ���带 ã�� �� �����ϴ�: " + name);
            return;
        }

        // ���� ���ǰ� ������ �ٽ� ������� ����
        if (musicSource.clip == s.clip && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("SFX ���带 ã�� �� �����ϴ�: " + name);
            return;
        }

        sfxSource.PlayOneShot(s.clip);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}


