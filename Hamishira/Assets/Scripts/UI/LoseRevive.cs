using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class LoseRevive : MonoBehaviour
{
    public GameObject LoseModal;
    public Text KillsText;
    public Text LevelText;
    public Text CoinText;
    public GameObject Hero;

    public void VerifyRevived() {
        StartCoroutine(ShowModal());
    }

    IEnumerator ShowModal() {
        Hero.GetComponent<Animator>().SetTrigger("Dead"); // Animation on dead
        yield return new WaitForSeconds(1f);
        OnEnd();
        LoseModal.SetActive(true);
        Time.timeScale = 0f;
    }
    
    void OnEnd() {
        LevelText.text = "Level: " + PlayerPrefs.GetInt("CurrentLevel").ToString();
        KillsText.text = "Kills: " + PlayerPrefs.GetInt("KilledMonsters").ToString();
        CoinText.text = "2X Reward: " + (PlayerPrefs.GetInt("LevelCollect") * 2).ToString() + " coins";
        SetRecord();
    }

    void SetRecord() {
        if (PlayerPrefs.GetInt("RecordKilledMonsters") != 0) {
            if (PlayerPrefs.GetInt("KilledMonsters") > PlayerPrefs.GetInt("RecordKilledMonsters")) {
                PlayerPrefs.SetInt("RecordKilledMonsters", PlayerPrefs.GetInt("KilledMonsters"));
            }
        } else {
            PlayerPrefs.SetInt("RecordKilledMonsters", PlayerPrefs.GetInt("KilledMonsters"));
        }
    }
}
