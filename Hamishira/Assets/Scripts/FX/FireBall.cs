using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Animator anim;
    private bool IsCollising;
    private bool IsCollisingHero;

    public int WhereLook;

    void Start() {

        anim = GetComponent<Animator>();
    }

    void Update() {
        if (!IsCollising)
            transform.position += (WhereLook * transform.right) * 10f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ground") || other.CompareTag("Player")) {
            anim.SetBool("IsCollision", true);
            IsCollising = true;
            Destroy(gameObject, .5f);
        } else if (other.CompareTag("Hero_Arm")) {
            anim.SetBool("IsCollisionOther", true);
            IsCollising = true;
            Destroy(gameObject, .5f);
        }

        if (other.CompareTag("Player")) {
            if (!IsCollisingHero) {
                int damage = Random.Range(20, 40);
                PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") - damage);
                IsCollisingHero = true;
            }
        }
    }
}
