using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCharacter : MonoBehaviour
{
    public GameObject Arm;
    public void StartDead() {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // Set Rigidbody static
        Collider2D[] colliders = GetComponents<Collider2D>(); // Get all colliders of enemy
        foreach( Collider2D collider in colliders ) { // Disable all colliders from this enemy
            collider.enabled = false;
        }

        if (Arm) {
            Arm.GetComponent<Collider2D>().enabled = false;
        }
    }
    // Destroy this
    public void EndDead() {
        Destroy(gameObject, .6f);
    }
}
