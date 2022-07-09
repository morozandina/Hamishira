using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnerAbilites : MonoBehaviour
{
    public GameObject Hero;
    public GameObject AbilityBlock;
    [Header ("      UI")]
    public GameObject AbilityUI;
    public GameObject ImageUI;
    public GameObject UltaUI;
    [Header ("      Standart")]
    public Ability[] Abilities;
    
    public GameObject ShowAbilitiesInfo;
    public Vector2[] AbilityPosition;

    [Header ("      Sound")]
    public AudioSource SpawnSound;

    private Coroutine coroutine;
    public static int orient;
    private bool isCharge;

    [System.Serializable]
    public class Ability {
        public Sprite abilitySprite;
        public int minProbability = 0;
        public int maxProbability = 0;
        public string abilityInfo;
        public string abilityIndex;
        public int id;
    }

    void Start() {
        StartCoroutine(SpawnAb(10f));
        UltaUI.GetComponent<Button>().onClick.AddListener(UltaUse);

        if (!Hero)
            Hero = GameObject.FindGameObjectWithTag("Player");

        LoadUI();
    }

    IEnumerator SpawnAb(float time) {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < AbilityPosition.Length; i++) {
            int random = Random.Range(0, 100);
            
            for (int j = 0; j < Abilities.Length; j++) {
                if (random >= Abilities[j].minProbability && random <= Abilities[j].maxProbability) {
                    var thisGo = Instantiate(AbilityBlock, AbilityPosition[i], Quaternion.identity, transform);
                    thisGo.GetComponent<SpriteRenderer>().sprite = Abilities[j].abilitySprite;
                    thisGo.GetComponent<AbilitiesInfo>().id = Abilities[j].id;
                    break;
                }
            }
        }
    }

    public void ShowInfoText(GameObject Hero) {
        SpawnSound.pitch = Random.Range(0.9f, 1.1f);
        SpawnSound.Play();

        GameObject damageText = Instantiate(ShowAbilitiesInfo, Hero.transform);
        damageText.GetComponentInChildren<TextMeshPro>().text = Abilities[orient].abilityInfo;
    }

    public void PickAbility() {
        ResetAll(); 
        if (orient == 0) {
            Hero.GetComponent<AbilityManager>().FireAbility();
        } else if (orient == 1) {
            ResetUltaPrefs();
            UltaUI.SetActive(true);
            Hero.GetComponent<AbilityManager>().AcidAbility();
            PlayerPrefs.SetInt("Ulta", 2);
        } else if (orient == 2) {
            Hero.GetComponent<AbilityManager>().InstantiateKill();
        } else if (orient == 3) {
            Hero.GetComponent<AbilityManager>().PlusSpeed();
        } else if (orient == 4) {
            Hero.GetComponent<AbilityManager>().ExtraJump();
        } else if (orient == 5) {
            ResetUltaPrefs();
            Hero.GetComponent<AbilityManager>().Restore50Hp();
            PlayerPrefs.SetInt("Ulta", 3);
            UltaUI.SetActive(true);
        } else if (orient == 6) {
            Hero.GetComponent<AbilityManager>().IncreseDamage();
        } else if (orient == 7) {
            Hero.GetComponent<AbilityManager>().IadAbility();
        } else if (orient == 8) {
            Hero.GetComponent<AbilityManager>().AddShield();
        } else if (orient == 9) {
            ResetUltaPrefs();
            Hero.GetComponent<AbilityManager>().AddShield();
            PlayerPrefs.SetInt("Ulta", 4);
            UltaUI.SetActive(true);
        } else if (orient == 10) {
            Hero.GetComponent<AbilityManager>().IncreseHP();
        } else if (orient == 11) {
            Hero.GetComponent<AbilityManager>().PlusSpeed1();
        } else if (orient == 12) {
            Hero.GetComponent<AbilityManager>().Stamina();
        } else if (orient == 13) {
            Hero.GetComponent<AbilityManager>().Heal(50);
        } else if (orient == 14) {
            Hero.GetComponent<AbilityManager>().Heal(100);
        } else if (orient == 15) {
            Hero.GetComponent<AbilityManager>().Heal(250);
        } else if (orient == 16) {
            ResetUltaPrefs();
            Hero.GetComponent<AbilityManager>().SecondSword();
            PlayerPrefs.SetInt("Ulta", 1);
            UltaUI.SetActive(true);
        }
        EnterToUI();
    }
    // Ability UI
    public void LoadUI() {
        for (int i = 0; i < Abilities.Length; i++) {
            if (PlayerPrefs.GetInt("AbilityCount" + i) > 0) {
                var instantiateImage = Instantiate(ImageUI, AbilityUI.transform);
                instantiateImage.GetComponent<Image>().sprite = Abilities[i].abilitySprite;
                if (PlayerPrefs.GetInt("AbilityCount" + i) > 1) {
                    if (Abilities[i].abilityIndex != "ulta") {
                        instantiateImage.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetInt("AbilityCount" + i).ToString();
                    }
                }
                if (Abilities[i].abilityIndex == "ulta") {
                    isCharge = true;
                    UltaUI.SetActive(true);
                    UltaUI.GetComponent<Button>().onClick.AddListener(UltaUse);
                }
            }
        }
    }

    public void EnterToUI() {
        bool ItIs = false;

        PlayerPrefs.SetInt("AbilityCount" + orient, PlayerPrefs.GetInt("AbilityCount" + orient) + 1);
        foreach(Transform child in AbilityUI.transform) {
            if (child.GetComponent<Image>().overrideSprite == Abilities[orient].abilitySprite) {
                if (Abilities[orient].abilityIndex != "ulta") {
                    child.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetInt("AbilityCount" + orient).ToString();
                }
                if (Abilities[orient].abilityIndex == "ulta") {
                    child.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }
                ItIs = true;
            }
        }

        if (!ItIs) {
            var instantiateImage = Instantiate(ImageUI, AbilityUI.transform);
            instantiateImage.GetComponent<Image>().sprite = Abilities[orient].abilitySprite;
        }
    }

    public void ResetUltaPrefs() {
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        
        if (PlayerPrefs.GetInt("Ulta") == 1) {
            Hero.GetComponent<SwordsInstantiate>().RemoveSecondSword();
            Hero.GetComponent<SwordsInstantiate>().changeSword = false;
        }
        isCharge = true;
        UltaUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        UltaUI.GetComponent<Button>().enabled = true;
    }

    public void UltaUse() {
        if (isCharge == true) {
            isCharge = false;
            UltaUI.GetComponent<Image>().color = new Color(0.200f, 0.200f, 0.200f, 1.000f);
            UltaUI.GetComponent<Button>().enabled = false;
            if (PlayerPrefs.GetInt("Ulta") == 1) {
                Hero.GetComponent<SwordsInstantiate>().GesticulateSword();
            }
            if (PlayerPrefs.GetInt("Ulta") == 2) {
                Hero.GetComponent<AbilityManager>().AcidAbility();
            }
            if (PlayerPrefs.GetInt("Ulta") == 3) {
                Hero.GetComponent<AbilityManager>().Restore50Hp();
            }
            if (PlayerPrefs.GetInt("Ulta") == 4) {
                Hero.GetComponent<AbilityManager>().AddShield();
            }
            // ShowInfoText(Hero);
            coroutine = StartCoroutine(ChargeUlta());
        }
    }

    IEnumerator ChargeUlta() {
        yield return new WaitForSeconds(15f);
        UltaUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        UltaUI.GetComponent<Button>().enabled = true;
        isCharge = true;
    }

    public void ResetAll() {
        foreach (Transform child in transform) {
            if (child.gameObject != SpawnSound.gameObject) {
                Destroy(child.gameObject);
            }
        }
        StartCoroutine(SpawnAb(60f));
    }
}