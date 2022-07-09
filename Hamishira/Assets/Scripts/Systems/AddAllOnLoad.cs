using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAllOnLoad : MonoBehaviour
{
    // Public variable
    [Header ("  For introducing statistics to UI")]
    public Text coinInfo;
    public Text HPInfo;
    public Text Xp_nr;
    public GameObject XP_Indicator;

    [Header ("  LevelUp")]
    public GameObject LevelUpFx;
    private Transform HeroPosition;

    void Start() {
        Spawner.TotalMonster = 0;
        Spawner.Boss = false;
        HeroPosition = GameObject.FindGameObjectWithTag("Player").transform;
        coinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
        Xp_nr.text = PlayerPrefs.GetInt("CurrentLevel").ToString();
        XP_Indicator.GetComponent<RectTransform>().transform.localScale = new Vector3(0f, 1f, 1f);
    }

    void Update() {
        // Upgrade
        if (PlayerPrefs.GetInt("CurrentHP") >= 0) {
            if (PlayerPrefs.GetInt("CurrentXP") >= PlayerPrefs.GetInt("MaxXP")) {
                // Start Anim
                var temp = Instantiate(LevelUpFx, HeroPosition);
                temp.GetComponent<AudioSource>().Play();
                Destroy(temp, 1f);
                // Max XP = 100 * (1.1) ^ level
                PlayerPrefs.SetInt("MaxXP", Mathf.RoundToInt(100 * Mathf.Pow(1.1f, PlayerPrefs.GetInt("CurrentLevel"))));
                PlayerPrefs.SetInt("CurrentXP", 0);
                // Damage
                PlayerPrefs.SetInt("PlusDamage", PlayerPrefs.GetInt("PlusDamage") + 5);
                // HP
                if (PlayerPrefs.GetInt("CurrentHP") < PlayerPrefs.GetInt("MaxHP"))
                    PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("MaxHP"));
                // Enemy
                PlayerPrefs.SetInt("EnemyHP", PlayerPrefs.GetInt("EnemyHP") + 10);
                PlayerPrefs.SetInt("EnemyDamage", PlayerPrefs.GetInt("EnemyDamage") + 10);
                // Level
                PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            }
            updateXP();
        }
    }

    void updateXP() {
        float tempCurrent = PlayerPrefs.GetInt("CurrentXP");
        float tempMax = PlayerPrefs.GetInt("MaxXP");
        float procentage = (tempCurrent / tempMax) * 100f;

        Xp_nr.text = PlayerPrefs.GetInt("CurrentLevel").ToString();
        XP_Indicator.GetComponent<RectTransform>().transform.localScale = new Vector3(procentage / 100f, 1f, 1f);

        coinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
        HPInfo.text = PlayerPrefs.GetInt("CurrentHP").ToString() + " / " + PlayerPrefs.GetInt("MaxHP");
    }
}
