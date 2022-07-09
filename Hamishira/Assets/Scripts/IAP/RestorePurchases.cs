using UnityEngine;

public class RestorePurchases : MonoBehaviour
{
    void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer || Application.platform != RuntimePlatform.OSXPlayer) {
            transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(1500, 0);
            gameObject.SetActive(false);
        }
    }

    public void ClickRestorePurchaseButton() {
        IAPManager.instance.RestorePurchases();
    }
}
