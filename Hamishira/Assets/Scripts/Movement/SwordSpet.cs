using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwordSpet : MonoBehaviour
{
    GameObject sword;

    void Start() {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        sword = GameObject.FindGameObjectWithTag("Hero_Arm");
    }

    public void OnClick() {
        if (sword != null) {
            sword.GetComponent<Rigidbody2D>().AddForce(transform.up * 800f);
            StartCoroutine(Delay());
            gameObject.GetComponent<Image>().color = new Color(0.200f, 0.200f, 0.200f, 1.000f);
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public IEnumerator Delay() {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gameObject.GetComponent<Button>().enabled = true;
    }
}
