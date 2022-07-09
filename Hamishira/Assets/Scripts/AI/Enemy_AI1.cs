using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI1 : MonoBehaviour
{
    // Other
    GameObject Hero;

    void Start() {
        Hero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (Hero != null) {
            if (Hero.transform.position.x < transform.position.x) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (Hero.transform.position.x > transform.position.x) {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            transform.position = Vector2.Lerp(transform.position, new Vector2(Hero.transform.position.x, Hero.transform.position.y), Time.deltaTime / 1.5f);
        }
    }
}
