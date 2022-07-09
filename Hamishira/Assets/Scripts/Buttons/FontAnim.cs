using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FontAnim : MonoBehaviour, IPointerUpHandler
{
    // For changing text sprite
    public Sprite TextNormal;
    public Sprite TextCut;

    // Character game objects for animation
    public GameObject Character;
    public GameObject Character1;
    public GameObject Character2;

    // Animation for characters
    Animator anim;
    Animator anim_1;
    Animator anim_2;

    // Start / Get animation from characters
    void Start() {
        anim = Character.GetComponent<Animator>();
        anim_1 = Character1.GetComponent<Animator>();
        anim_2 = Character2.GetComponent<Animator>();
    }
    
    // On click button and verification
    public void OnPointerUp(PointerEventData eventData) {
        if (Camera.main) {
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        }
        
        StartCoroutine(playAnimation(this.gameObject));
        anim_1.SetTrigger("Play");
        if (this.gameObject.name == "Play") {
            anim.SetTrigger("Play");
        }
        if (this.gameObject.name == "Shop") {
            anim.SetTrigger("Shop");
        }
        if (this.gameObject.name == "About") {
            anim.SetTrigger("About");
        }
    }
    
    // Ienumerator for delay on changing text sprite
    public IEnumerator playAnimation(GameObject thisObj) {
        yield return new WaitForSeconds(.1f);
        thisObj.GetComponent<Button>().image.sprite = TextCut;
    }
}
