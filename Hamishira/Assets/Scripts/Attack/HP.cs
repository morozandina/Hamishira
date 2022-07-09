using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP : MonoBehaviour
{
    // Local variable
    Animator anim;
    Rigidbody2D rb;

    [Header ("  HP")]
    // Public game objects
    public GameObject[] Heart;
    public Sprite[] Inim;

    [Header ("  For Hit")]
    // Other
    GameObject Hero;
    GameObject Sword;

    [Header ("  Animation")]
    // For animation text on hit
    public GameObject FloatingTextPrefab;
    public GameObject Blood;
    public GameObject Fire;

    [Header ("  Parameter")]
    // Other variable
    public int maxHp;
    public int damageToOther;

    [Header ("  Sounds")]
    public AudioSource HitSound;
    public AudioSource KillSound;

    Camera cam;
    // Simple variable
    int hp;
    // Public variable
    public static int XPOnDead;
    public static bool AddFire;
    public static bool AddIad;
    public static bool KillInstantiate;

    bool verifyGO;
    // When Start
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        cam = Camera.main;
        hp = maxHp;
        XPOnDead = Random.Range(10, 15);

        // Set default variable
        Hero = GameObject.FindGameObjectWithTag("Player");
        Sword = GameObject.FindGameObjectWithTag("Hero_Arm");

        // Simple verification
        if (gameObject.GetComponent<Collider2D>().isTrigger) {
            verifyGO = true;
        }
    }

    // Verify HP and attack enemy
    void OnCollisionEnter2D(Collision2D collision) {
        if (Sword != null) {
            if (Sword.gameObject.name == collision.gameObject.name) {
                // Damage relative speed and force
                Vector3 impactVelocity = collision.relativeVelocity;
                float damage = Mathf.Max(0f, impactVelocity.magnitude) + PlayerPrefs.GetInt("PlusDamage");
                damage += PlayerPrefs.GetInt("PreocentageUpgradeDamage");
                // Give Damage
                if (maxHp >= 0) {
                    HitSoundEffect();
                    FireAdd();
                    IadAdd();

                    hp -= (int)damage;
                    GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                    damageText.GetComponentInChildren<TextMeshPro>().text = ((int)damage).ToString();
                    PushAway((int)damage * 5);
                    destroyComplectly();
                    CheckHeart();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Fire_0" || collision.gameObject.name == "Fire_0(Clone)" || collision.gameObject.name == "Acid(Clone)") {
            if (maxHp >= 0) {
                HitSoundEffect();
                hp -= 15;
                GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                damageText.GetComponentInChildren<TextMeshPro>().text = (15).ToString();
                // PushAway();
                CheckHeart();
            }
        }
        if (collision.gameObject.name == "Bounds") {
            if (maxHp >= 0) {
                hp -= hp;
                GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                damageText.GetComponentInChildren<TextMeshPro>().text = (hp).ToString();
                // PushAway();
                CheckHeart();
            }
        }
        // Hit Ghost
        if (verifyGO) {
            if (Sword != null) {
                if (Sword.gameObject.name == collision.gameObject.name) {
                    HitSoundEffect();
                    float damage = Random.Range(10, 15) + PlayerPrefs.GetInt("PlusDamage");
                    damage += PlayerPrefs.GetInt("PreocentageUpgradeDamage");
                    // Give Damage
                    if (maxHp >= 0) {
                        
                        FireAdd();
                        IadAdd();

                        hp -= (int)damage;
                        GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                        damageText.GetComponentInChildren<TextMeshPro>().text = ((int)damage).ToString();
                        PushAway((int)damage * 10);
                        destroyComplectly();
                        CheckHeart();
                    }
                }
            }
        }
    }

    // All Sound Effect
    private void HitSoundEffect() {
        HitSound.pitch = Random.Range(0.9f, 1.1f);
        HitSound.Play();
    }

    private void KillSoundEffect() {
        KillSound.pitch = Random.Range(0.9f, 1.1f);
        KillSound.Play();
    }

    // Check Heart Status
    void CheckHeart() {
        if (0 >= hp) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[2];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[2];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[2];
            // When dead enemy
            OnDeadEnemy(this.gameObject);
        } else if ( maxHp * 0.14f >= hp ) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[1];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[2];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[2];
        } else if ( maxHp * 0.32f >= hp ) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[2];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[2];
        } else if ( maxHp * 0.48f >= hp ) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[1];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[2];
        } else if ( maxHp * 0.64f >= hp ) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[2];
        } else if ( maxHp * 0.8f >= hp ) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[1];
        } else if ( maxHp >= hp ) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[0];
        }
    }

    // Set animation on hit and push power
    void PushAway(float pushPower) {
        if (cam) {
            cam.GetComponent<Animator>().SetTrigger("Shake");
        }
        if (rb == null || pushPower == 0) {
            return;
        }
        if (Hero.transform.position.x < transform.position.x) {
            rb.AddForce((transform.right * 10)* pushPower);
            StartCoroutine(hitAnimation());
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        if (Hero.transform.position.x > transform.position.x) {
            rb.AddForce((transform.right * -10)* pushPower);
            StartCoroutine(hitAnimation());
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
    }

    // IEnumerator for hit animation
    public IEnumerator hitAnimation() {
        yield return new WaitForSeconds(.2f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    
    // All actiunes when caracther dead
    public void OnDeadEnemy(GameObject enemy) {
        KillSoundEffect();
        Instantiate(Blood, (transform.position + new Vector3(0, 1f, 0)), Quaternion.identity);
        anim.SetTrigger("Dead"); // Animation on dead
        // If is Boss
        if (gameObject.GetComponent<BossControll>() != null) {
            GetComponent<BossControll>().OnFinished();
        }
        // Mob XP = mobXp * (1.1) ^ level - 10
        var tempXp = PlayerPrefs.GetInt("CurrentXP") + Mathf.RoundToInt(XPOnDead * Mathf.Pow(1.1f, Mathf.Abs(PlayerPrefs.GetInt("CurrentLevel") - 10)));
        var tempProcentage = tempXp * PlayerPrefs.GetFloat("PreocentageUpgradeXP");
        PlayerPrefs.SetInt("CurrentXP", tempXp + (int)tempProcentage); // Add Xp to character
        PlayerPrefs.SetInt("KilledMonsters", PlayerPrefs.GetInt("KilledMonsters") + 1); // Add kills for end Menu
        Spawner.TotalMonster--;
        int collected = Random.Range(10, 50);

        PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") + collected);
        PlayerPrefs.SetInt("LevelCollect", PlayerPrefs.GetInt("LevelCollect") + collected);
    }

    // Ability
    public void FireAdd() {
        if (AddFire == true) {
            var go = Instantiate(Fire, transform.position, Quaternion.identity);
            Destroy(go, 15f);
            AddFire = false;
        }
    }

    public void destroyComplectly() {
        if (KillInstantiate == true) {
            hp -= hp;
            KillInstantiate = false;
        }
    }

    public void IadAdd() {
        if (AddIad == true) {
            StartCoroutine(IadHp());
            AddIad = false;
        }
    }

    IEnumerator IadHp() {
        int n = 0;
        while (n < 30) {
            GetComponent<SpriteRenderer>().color = new Color(0, .95f, 0, 1);
            hp -= 2;
            GameObject damageText = Instantiate(FloatingTextPrefab, transform);
            damageText.GetComponentInChildren<TextMeshPro>().text = (2).ToString();
            n++;
            yield return new WaitForSeconds(.25f);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
