using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SasukeSword : MonoBehaviour
{
    public GameObject DarkFire;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy_Arm")) {
            InstantiateFire(other.gameObject.transform);
        }
    }

    public bool GetRandom() {
        int random = Random.Range(0, 100);
        if (random >= 95 && random <= 100) {
            return true;
        } else {
            return false;
        }

    }

    public void InstantiateFire(Transform EnemyPos) {
        if (GetRandom()) {
            var go = Instantiate(DarkFire, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            Destroy(go, 4f);
        }
    }
}
