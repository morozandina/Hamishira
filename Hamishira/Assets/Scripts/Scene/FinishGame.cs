using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FinishGame : MonoBehaviour
{
    public GameObject WinPanel;

    void OnEnable() {
        if (PlayerPrefs.GetInt("IfFinished") == 1) {
            if (PlayerPrefs.GetInt("IfShowed") == 0) {
                WinPanel.SetActive(true);
                PlayerPrefs.SetInt("IfShowed", 1);
            }
        }
    }
}
