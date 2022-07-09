using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FontAnimExit : MonoBehaviour, IPointerUpHandler
{
    public GameObject[] TextNormalButton;
    public Sprite[] TextCut;

    public GameObject Character;
    public GameObject Character1;
    Animator anim;
    Animator anim_1;

    void Start() {
        anim = Character.GetComponent<Animator>();
        anim_1 = Character1.GetComponent<Animator>();
    }
    
    public void OnPointerUp(PointerEventData eventData) {
        StartCoroutine(playAnimation());
        anim_1.SetTrigger("Play");
        if (this.gameObject.name == "Exit") {
            anim.SetTrigger("Exit");
        }
    }

    public IEnumerator playAnimation() {
        yield return new WaitForSeconds(.2f);

        for (var i = 0; i < TextNormalButton.Length; i++) {
            TextNormalButton[i].GetComponent<Button>().image.sprite = TextCut[i];
        }

        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }
}
