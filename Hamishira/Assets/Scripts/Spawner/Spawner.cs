using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("   Instantiate")]
    public GameObject[] Enemies;
    [Header("   Time")]
    public float afterTime;
    public float betweenTime;

    public static bool Boss;
    public static int TotalMonster;

    void Start() {
        InvokeRepeating("SpawnEnemy", afterTime, betweenTime);
    }

    public void SpawnEnemy() {
        if (TotalMonster < 20) {
            if (Boss == true) {
                CancelInvoke("SpawnEnemy");
            } else {
                int random = Random.Range(0, Enemies.Length);
                GameObject temp = Instantiate(Enemies[random], transform.position, Quaternion.identity, transform);
                temp.GetComponent<HP>().maxHp += PlayerPrefs.GetInt("EnemyHP");
                temp.GetComponent<HP>().damageToOther += PlayerPrefs.GetInt("EnemyDamage");
                TotalMonster++;
            }
        }
    }
}
