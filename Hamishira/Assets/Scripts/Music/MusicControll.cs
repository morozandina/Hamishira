using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MusicControll : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public Image BGControll;
    public Image FXControll;

    void Start() {
        FX(PlayerPrefs.GetInt("FXSound"));
        Music(PlayerPrefs.GetInt("Music"));
    }

    public void ChangeMusic() {
        int nr = PlayerPrefs.GetInt("Music");
        if (nr == 0)
            PlayerPrefs.SetInt("Music", 1);
        else
            PlayerPrefs.SetInt("Music", 0);
        Music(PlayerPrefs.GetInt("Music"));
    }

    public void ChangeFX() {
        int nr = PlayerPrefs.GetInt("FXSound");
        if (nr == 0)
            PlayerPrefs.SetInt("FXSound", 1);
        else
            PlayerPrefs.SetInt("FXSound", 0);
        FX(PlayerPrefs.GetInt("FXSound"));
    }

    public void FX(int nr) {
        if (nr == 1) {
            Mixer.audioMixer.SetFloat("FXVolume", -80);
            Mixer.audioMixer.SetFloat("UIVolume", -80);
            FXControll.color = new Color(0.314f, 0.314f, 0.314f, 1.000f);
        } else {
            Mixer.audioMixer.SetFloat("FXVolume", 10);
            Mixer.audioMixer.SetFloat("UIVolume", -10);
            FXControll.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void Music(int nr) {
        if (nr == 1) {
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
            BGControll.color = new Color(0.314f, 0.314f, 0.314f, 1.000f);
        } else {
            Mixer.audioMixer.SetFloat("MusicVolume", 0);
            BGControll.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
