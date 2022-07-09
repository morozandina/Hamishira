using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;

public class ShopManager : MonoBehaviour, IUnityAdsListener
{
    // private string[] confirmationText = {"Buy 1% HP for 100 coins ?", "Buy this sword for 100 coins ?"};
    public Text coinInfo;
    [Header ("      Confirmation Modal")]
    public GameObject AlertPannel;
    public GameObject ConfirmationPannel;
    public Text ConfirmationText;

    [Header ("      Upgrade Component")]
    public List<GameObject> Upgrade;
    private List<Text> UpgradePrece = new List<Text>();
    public List<GameObject> Swords;
    public List<Text> SwordsPrece = new List<Text>();
    // Upgrade Variable
    private int Prece = 100;
    private static string whatIsBough;

    public AudioSource SwordSound;
    public AudioSource CoinSound;
    // Start function
    void Start() {
        if (Advertisement.isSupported) {
            Advertisement.Initialize("4152591", false);
        }

        if (PlayerPrefs.GetInt("AdCount") >= 100) {
            PlayerPrefs.SetInt("LockedSword" + 8, 1);
        }

        SetSwordButton();

        for (int i = 0; i < Upgrade.Count; i++) {
            UpgradePrece.Add(Upgrade[i].transform.GetChild(0).GetComponent<Text>());
        }
        // Load all
        OnLoad();
        OnLoadSwords();
    }

    // On Load prece on Upgrade (Update)
    public void OnLoad() {
        coinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
        UpgradePrece[0].text = ((int)(Prece + PlayerPrefs.GetInt("PreocentageUpgradeHP") * 4)).ToString();
        UpgradePrece[1].text = ((int)(Prece + PlayerPrefs.GetFloat("PreocentageUpgradeXP") * 1000)).ToString();
        UpgradePrece[2].text = ((int)(Prece + PlayerPrefs.GetInt("PreocentageUpgradeDamage") * 100)).ToString();
    }
    // On Load prece on Swords (Update)
    public void OnLoadSwords() {
        coinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
        for (int i = 0; i < SwordsPrece.Count; i++) {
            if (PlayerPrefs.GetInt("LockedSword" + i) == 1) {
                SwordsPrece[i].text = "Use";
            }
            if (PlayerPrefs.GetInt("CurrentSword") == i) {
                SwordsPrece[i].text = "Used";
            }
        }
        if (PlayerPrefs.GetInt("AdCount") < 100) {
            SwordsPrece[8].text = PlayerPrefs.GetInt("AdCount") + " / 100\nAds";
        }
    }
    
    // Sword Button settings
    public void SetSwordButton() {
        for (int i = 0; i < Swords.Count; i++) {
            int k = i;
            Swords[i].GetComponent<Button>().onClick.AddListener(() => OnClikSword(k));
        }
    }

    public void OnClikSword(int index) {
        SwordSound.pitch = Random.Range(0.9f, 1.1f);
        SwordSound.Play();
        string CurrentStatus = SwordsPrece[index].text;
        if (TransformToInt(CurrentStatus) == 0) {
            if (CurrentStatus == "Use") {
                PlayerPrefs.SetInt("CurrentSword", index);
                OnLoadSwords();
            }
        } else {
            whatIsBough = index.ToString();
            ConfirmationPannel.SetActive(true);
            ConfirmationText.text = "Buy this sword for " + CurrentStatus + " coins ?";
        }
    }

    // Buy Change Choose
    public void OnWantBuy() {
        var What = EventSystem.current.currentSelectedGameObject.name;
        SwordSound.pitch = Random.Range(0.9f, 1.1f);
        SwordSound.Play();
        switch (What) {
            case "HP":
                whatIsBough = "HP";
                ConfirmationText.text = "+ 25 " + What + " for " + UpgradePrece[0].text + " coins ?";
                break;
            case "XP":
                whatIsBough = "XP";
                ConfirmationText.text = "+ 1 % " + What + " for " + UpgradePrece[1].text + " coins ?";
                break;
            case "Damage":
                whatIsBough = "Damage";
                ConfirmationText.text = "+ 1 " + What + " for " + UpgradePrece[2].text + " coins ?";
                break;
            case "RewadedAd":
                whatIsBough = "RewadedAd";
                ConfirmationText.text = "Watch add for " + "250" + " coins ?";
                break;
            case "Coin1":
                whatIsBough = "Coin1";
                ConfirmationText.text = "Buy 5.000 coin for " + "0.99" + " USD ?";
                break;
            case "Coin2":
                whatIsBough = "Coin2";
                ConfirmationText.text = "Buy 10.000 coin for " + "1.99" + " USD ?";
                break;
            case "Coin3":
                whatIsBough = "Coin3";
                ConfirmationText.text = "Buy 100.000 coin for " + "6.99" + " USD ?";
                break;
            case "Coin4":
                whatIsBough = "Coin4";
                ConfirmationText.text = "Unlock all Swords for " + "9.99" + " USD ?";
                break;
            case "Coin5":
                whatIsBough = "Coin5";
                ConfirmationText.text = "Upgrade by 25 to all for " + "14.99" + " USD ?";
                break;
        }

        ConfirmationPannel.SetActive(true);
    }

    // // Confirmation
    public void Confirmation() {
        var What = EventSystem.current.currentSelectedGameObject.name;
        switch (What) {
            case "No":
                SwordSound.pitch = Random.Range(0.9f, 1.1f);
                SwordSound.Play();
                break;
            case "Yes":
                VerificationWhatBuy();
                break;
        }

        ConfirmationPannel.SetActive(false);
    }

    // Buy verification
    private void VerificationWhatBuy() {
        if (whatIsBough == "HP") {
            if (VerifyBuy(UpgradePrece[0].text)) {
                PlayerPrefs.SetInt("PreocentageUpgradeHP", PlayerPrefs.GetInt("PreocentageUpgradeHP") + 25);
            }
        } else if (whatIsBough == "XP") {
            if (VerifyBuy(UpgradePrece[1].text)) {
                PlayerPrefs.SetFloat("PreocentageUpgradeXP", PlayerPrefs.GetFloat("PreocentageUpgradeXP") + .1f);
            }
        } else if (whatIsBough == "Damage") {
            if (VerifyBuy(UpgradePrece[2].text)) {
                PlayerPrefs.SetInt("PreocentageUpgradeDamage", PlayerPrefs.GetInt("PreocentageUpgradeDamage") + 1);
            }
        } else if (whatIsBough == "RewadedAd") {
            if (Advertisement.IsReady("hamishiraGetCoin")) {
                Advertisement.Show("hamishiraGetCoin");
                Advertisement.AddListener(this);
            }
        } else {
            for (int i = 0; i < Swords.Count; i++) {
                if (whatIsBough == i.ToString()) {
                    if (VerifyBuy(SwordsPrece[i].text)) {
                        PlayerPrefs.SetInt("LockedSword" + i, 1);
                    }
                }
            }
        }
        // Loaded all
        OnLoad();
        OnLoadSwords();
    }

    // Buy verification if Money is enough
    private bool VerifyBuy(string sum) {
        if (PlayerPrefs.GetInt("CoinKey") < TransformToInt(sum)) {
            AlertPannel.SetActive(true);
            return false;
        } else {
            CoinSound.pitch = Random.Range(0.9f, 1.1f);
            CoinSound.Play();
            PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") - TransformToInt(sum));
            return true;
        }
    }
    // String to Int
    private int TransformToInt(string sum) {
        int NewSum = 0;
        if (int.TryParse(sum, out NewSum)) {
            return NewSum;
        } else {
            return 0;
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string surfacingId, ShowResult showResult) {
        Debug.Log(surfacingId);
        Debug.Log(showResult);
        if (showResult == ShowResult.Finished) {
            PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") + 250);
            PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);
            coinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
            OnLoadSwords();
        }
        else if (showResult == ShowResult.Skipped) {}
        else if (showResult == ShowResult.Failed) {}
    }

    public void OnUnityAdsReady (string surfacingId) {}

    public void OnUnityAdsDidError (string message) {}

    public void OnUnityAdsDidStart (string surfacingId) {}
}
