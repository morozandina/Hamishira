using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Jump_Button : MonoBehaviour, IPointerDownHandler
{
    GameObject character;

    void Start() {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (character != null) {
            character.GetComponent<Move>().Jump();
        }
    }
}
