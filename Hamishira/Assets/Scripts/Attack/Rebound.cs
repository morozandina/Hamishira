using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    public AudioSource SwordRebound;
    GameObject Sword;
    GameObject Hero;

    Rigidbody2D rb;

    void Start() {
        Sword = GameObject.FindGameObjectWithTag("Hero_Arm");
        Hero = GameObject.FindGameObjectWithTag("Player");

        rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (Sword != null) {
            if (Sword.gameObject.name == collision.gameObject.name) {
                PushTogether(Hero, 1000f);
                SwordRebound.pitch = Random.Range(0.9f, 1.1f);
                SwordRebound.Play();
            }
        }
        if (collision.gameObject.tag == "Enemy") {
            PushTogether(collision.gameObject, 500f);
        }
    }

    void PushTogether(GameObject GO, float force) {
        if (GO.transform.position.x < transform.position.x) {
            rb.AddForce(transform.right * force);
        }
        if (GO.transform.position.x > transform.parent.position.x) {
            rb.AddForce(transform.right * -force);
        }
    }
}
