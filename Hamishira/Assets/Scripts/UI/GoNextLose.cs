using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoNextLose : MonoBehaviour
{
    public GameObject NextPanel;
    public Text KillsText;
    public Text LevelText;

    public void GoNext() {
        NextPanel.SetActive(true);
        LevelText.text = "Level: " + PlayerPrefs.GetInt("CurrentLevel").ToString();
        KillsText.text = "Kills: " + PlayerPrefs.GetInt("KilledMonsters").ToString();
        gameObject.SetActive(false);
    }
}
