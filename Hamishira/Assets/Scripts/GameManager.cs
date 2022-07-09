using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header ("  Statistics")]
    public int RequiredLevel;
    public int LevelNumber;
    public Text Status;
    public GameObject Ability;

    [Header ("  Prefabs")]
    public GameObject Portal;

    [Header ("  Boss Date")]
    public GameObject Boss;
    public Vector2 BossPosition;

    [Header ("  Sounds")]
    public GameObject BossApear;

    private int Difference;
    private bool isBoss;

    void Awake() {
        // 1 - Earth; 0 - Hell; 2 - Paradise;
        PlayerPrefs.SetInt("LevelScene", LevelNumber);
    }

    void Start() {
        // Difference =  PlayerPrefs.GetInt("CurrentLevel");
    }

    void FixedUpdate() {
        if (!isBoss) {
            if (PlayerPrefs.GetInt("CurrentLevel") >= RequiredLevel) {
                Status.text = "Kill the Boss";
                GameObject boss = Instantiate(Boss, BossPosition, Quaternion.identity, transform);
                GameObject BossSound = Instantiate(BossApear, boss.transform);
                BossSound.GetComponent<AudioSource>().Play();
                isBoss = true;
                Spawner.Boss = true;
                // Ability.SetActive(false);
            } else {
                Status.text = PlayerPrefs.GetInt("CurrentLevel") + " / " + RequiredLevel + " level";
            }
        }
    }

    public void FinishLevel() {
        Status.text = "Enter to Portal";
        Portal.SetActive(true);
    }
}
