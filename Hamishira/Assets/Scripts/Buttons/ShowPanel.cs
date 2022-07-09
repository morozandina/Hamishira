using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject Lose;
    GameObject Hero;
    
    void Start() {
        Hero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (Hero == null) {
            Lose.SetActive(true);
        }
    }
}
