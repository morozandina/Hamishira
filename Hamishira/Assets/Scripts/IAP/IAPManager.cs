using System;
using UnityEngine;
using UnityEngine.Purchasing;


public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string coins_5000 = "hamishira.coins_5000";
    private string coins_10000 = "hamishira.coins_10000";
    private string coins_100000 = "hamishira.coins_100000";
    private string unlock = "hamishira.unlock";
    private string upgrade = "hamishira.upgrade";


    //************************** Adjust these methods **************************************
    public void InitializePurchasing() {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(coins_5000, ProductType.Consumable);
        builder.AddProduct(coins_10000, ProductType.Consumable);
        builder.AddProduct(coins_100000, ProductType.Consumable);
        builder.AddProduct(unlock, ProductType.NonConsumable);
        builder.AddProduct(upgrade, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized() {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void BuyCoins_5000() {
        BuyProductID(coins_5000);
    }
    public void BuyCoins_10000() {
        BuyProductID(coins_10000);
    }
    public void BuyCoins_100000() {
        BuyProductID(coins_100000);
    }
    public void BuyUnlock() {
        BuyProductID(unlock);
    }
    public void BuyUpgrade() {
        BuyProductID(upgrade);
    }




    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, coins_5000, StringComparison.Ordinal)) {
            PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") + 5000);
        } else if (String.Equals(args.purchasedProduct.definition.id, coins_10000, StringComparison.Ordinal)) {
            PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") + 10000);
        } else if (String.Equals(args.purchasedProduct.definition.id, coins_100000, StringComparison.Ordinal)) {
            PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") + 100000);
        } else if (String.Equals(args.purchasedProduct.definition.id, unlock, StringComparison.Ordinal)) {
            for (int i = 0; i < 8; i++) {
                PlayerPrefs.SetInt("LockedSword" + i, 1);
            }
        } else if (String.Equals(args.purchasedProduct.definition.id, upgrade, StringComparison.Ordinal)) {
            PlayerPrefs.SetInt("PreocentageUpgradeHP", PlayerPrefs.GetInt("PreocentageUpgradeHP") + (10 * 25));
            PlayerPrefs.SetFloat("PreocentageUpgradeXP", PlayerPrefs.GetFloat("PreocentageUpgradeXP") + (.1f * 25));
            PlayerPrefs.SetInt("PreocentageUpgradeDamage", PlayerPrefs.GetInt("PreocentageUpgradeDamage") + (2 * 25));
        } else {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake() {
        TestSingleton();
    }

    void Start() {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton() {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId) {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}