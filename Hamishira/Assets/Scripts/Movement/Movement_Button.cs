using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Movement_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject character;

    void Start() {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (character != null) {
            if (gameObject.name == "Left") {
                character.GetComponent<Move>().speed = -6f;
                character.GetComponent<Move>().acceleration = -5f;
            } else if (gameObject.name == "Right") {
                character.GetComponent<Move>().speed = 6f;
                character.GetComponent<Move>().acceleration = 5f;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (character != null) {
            character.GetComponent<Move>().speed = 0;
            character.GetComponent<Move>().acceleration = 0;
        }
    }
}
