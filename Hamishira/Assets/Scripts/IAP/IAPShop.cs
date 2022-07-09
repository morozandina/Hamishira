using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class IAPShop : MonoBehaviour
{
    public AudioSource OnClickSound;
    public GameObject ShopManager;
    public Text CoinInfo;

    public enum PurchaseType {coins_5000, coins_10000, coins_100000, unlock, upgrade}
    public PurchaseType purchaseType;

    public void OnPurchase() {
        OnClickSound.pitch = Random.Range(0.9f, 1.1f);
        OnClickSound.Play();

        switch(purchaseType) {
            case PurchaseType.coins_5000:
                IAPManager.instance.BuyCoins_5000();
                CoinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
                break;
            case PurchaseType.coins_10000:
                IAPManager.instance.BuyCoins_10000();
                CoinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
                break;
            case PurchaseType.coins_100000:
                IAPManager.instance.BuyCoins_100000();
                CoinInfo.text = PlayerPrefs.GetInt("CoinKey").ToString();
                break;
            case PurchaseType.unlock:
                IAPManager.instance.BuyUnlock();
                ShopManager.GetComponent<ShopManager>().OnLoadSwords();
                break;
            case PurchaseType.upgrade:
                IAPManager.instance.BuyUpgrade();
                ShopManager.GetComponent<ShopManager>().OnLoad();
                break;
        }
    }

            // RestorePurcahseBtn.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(1500, 0);
            // RestorePurcahseBtn.SetActive(false);
}
