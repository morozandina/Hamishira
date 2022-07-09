using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RestartButton : MonoBehaviour
{
    public ButtonController ButtonController;

    void Start() {
        if (Advertisement.isSupported) {
            Advertisement.Initialize("4152591", false);
        }
    }
    // On restart
    public void Restart() {
        Time.timeScale = 1.0f;
        if (Advertisement.IsReady("hamishiraRestart")) {
            Advertisement.Show("hamishiraRestart");
            PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);
        } 
        StartCoroutine(ButtonController.LoadLevel(3));
    }
}
