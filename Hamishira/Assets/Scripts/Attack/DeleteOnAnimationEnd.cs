using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnAnimationEnd : MonoBehaviour
{
    public float timeToDelete;

    public void Start() {
        Destroy(gameObject, timeToDelete);
    }
}
