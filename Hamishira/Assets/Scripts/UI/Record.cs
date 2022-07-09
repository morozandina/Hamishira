using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    void Start() {
        GetComponent<Text>().text = PlayerPrefs.GetInt("RecordKilledMonsters").ToString() + " kills";
    }
}
