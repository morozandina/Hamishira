using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesInfo : MonoBehaviour
{
    private GameObject Parent;
    private int nCheck = 0;
    public int id;

    void Start() {
        Parent = transform.parent.gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            if (nCheck < 1) {
                SpawnerAbilites.orient = id;
                Parent.GetComponent<SpawnerAbilites>().ShowInfoText(collider.gameObject);
                Parent.GetComponent<SpawnerAbilites>().PickAbility();
                nCheck++;
            }
        }
    }
}
