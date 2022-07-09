using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsInstantiate : MonoBehaviour
{
    public List<GameObject> Swords;
    private GameObject InstantiateSecondSword;
    public bool changeSword;
    
    void Awake() {
        GameObject sword = Instantiate(Swords[PlayerPrefs.GetInt("CurrentSword")], transform);
        sword.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();

        if (PlayerPrefs.GetInt("SecondSword") > 0) {
            SecondSword();
        }
    }

    public void SecondSword() {
        InstantiateSecondSword = Instantiate(Swords[PlayerPrefs.GetInt("CurrentSword")], transform);
        Vector2 pos = InstantiateSecondSword.transform.localPosition;
        InstantiateSecondSword.transform.localPosition = new Vector2(pos.x * -1, pos.y);
        InstantiateSecondSword.transform.localScale = new Vector2(-.8f, .8f);
        InstantiateSecondSword.transform.Rotate(0, 0, -180);
        InstantiateSecondSword.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
        InstantiateSecondSword.GetComponent<SpriteRenderer>().sortingOrder = 14;
    }

    public void RemoveSecondSword() {
        Destroy(InstantiateSecondSword);
    }

    public void GesticulateSword() {
        changeSword = !changeSword;

        if (changeSword) {
            RemoveSecondSword();
        } else if (!changeSword) {
            SecondSword();
        }
    }
}
