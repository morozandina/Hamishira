using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite Pressed, Default;
    // Animation on press button
    public void OnPointerDown(PointerEventData eventData) {
        GetComponent<Button>().image.sprite = Pressed;
    }
    public void OnPointerUp(PointerEventData eventData) {
        GetComponent<Button>().image.sprite = Default;
    }
}
