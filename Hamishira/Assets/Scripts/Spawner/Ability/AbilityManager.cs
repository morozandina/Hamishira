using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header ("      Abitlity component")]
    public GameObject Acid;
    public GameObject ShieldHeart;

    // Ability
    // 0
    public void FireAbility() {
        HP.AddFire = true;
    }
    // 1
    public void AcidAbility() {
        StartCoroutine(InstantiateAcid());
    }
    IEnumerator InstantiateAcid() {
        int n = 0;
        while (n < 7) {
            yield return new WaitForSeconds(.2f);
            var go = Instantiate(Acid, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            Destroy(go, 10f);
            n++;
        }
    }
    // 2
    public void InstantiateKill() {
        HP.KillInstantiate = true;
    }
    // 3
    public void PlusSpeed() {
        PlayerPrefs.SetFloat("ExtraSpeed", PlayerPrefs.GetFloat("ExtraSpeed") + 0.002f);
    }
    // 4
    public void ExtraJump() {
        PlayerPrefs.SetInt("ExtraJump", PlayerPrefs.GetInt("ExtraJump") + 1);
    }
    // 5
    public void Restore50Hp() {
        PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") + (PlayerPrefs.GetInt("MaxHP") / 2));
        GetComponent<Hero_HP>().CheckHeart();
    }
    // 6
    public void IncreseDamage() {
        PlayerPrefs.SetInt("PlusDamage", PlayerPrefs.GetInt("PlusDamage") + 1);
    }
    // 7
    public void IadAbility() {
        HP.AddIad = true;
    }
    // 8, 9
    public void AddShield() {
        ShieldHeart.SetActive(true);
        Hero_HP.Shield = true;
    }

    public void RemoveShield() {
        ShieldHeart.SetActive(false);
        Hero_HP.Shield = false;
    }

    // 10
    public void IncreseHP() {
        PlayerPrefs.SetInt("MaxHP", PlayerPrefs.GetInt("MaxHP") + 50);
        GetComponent<Hero_HP>().CheckHeart();
    }
    // 11
    public void PlusSpeed1() {
        PlayerPrefs.SetFloat("ExtraSpeed", PlayerPrefs.GetFloat("ExtraSpeed") + 0.005f);
    }
    // 12
    public void Stamina() {
        PlayerPrefs.SetInt("MinusDamage", PlayerPrefs.GetInt("MinusDamage") + 1);
    }
    // 13, 14, 15
    public void Heal(int val) {
        PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") + val);
        GetComponent<Hero_HP>().CheckHeart();
    }
    // 16
    public void SecondSword() {
        GetComponent<SwordsInstantiate>().SecondSword();
    }
}