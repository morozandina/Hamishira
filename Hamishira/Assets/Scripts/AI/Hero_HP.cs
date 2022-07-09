using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hero_HP : MonoBehaviour
{
    // Local variable
    Animator anim;
    Rigidbody2D rb;

    [Header ("  HP")]
    // Public game objects
    public GameObject[] Heart;
    public Sprite[] Inim;

    [Header ("  Animation")]
    // For animation text on hit
    public GameObject FloatingTextPrefab;
    public LayerMask LayerToHit;
    // Convas
    public GameObject Lose;

    [Header ("  Parameter")]
    // Other variable
    public int maxHp;

    [Header ("  Sounds")]
    public AudioSource HitSound;
    public AudioSource KillSound;
    
    int damage;
    int hp;
    bool isDead;
    bool Invincible;
    Vector3 StartPosition;

    public static bool LoseFlag;
    public static bool Shield;

    void Start() {
        isDead = false;
        maxHp = PlayerPrefs.GetInt("MaxHP");
        hp = PlayerPrefs.GetInt("CurrentHP");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CheckHeart();
        StartPosition = transform.position;
    }

    void Update() {
        if (!isDead) {
            if (hp <= 0) {
                CheckHeart();
            }
        }
    }

    void FixedUpdate() {
        maxHp = PlayerPrefs.GetInt("MaxHP");
        hp = PlayerPrefs.GetInt("CurrentHP");
        if (maxHp <= hp) {
            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[0];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[0];
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (Invincible == false) {
            if (collision.gameObject.tag == "Enemy_Arm" && Shield == false) {
                if (collision.gameObject.GetComponent<HP>()) {
                    damage = collision.gameObject.GetComponent<HP>().damageToOther - PlayerPrefs.GetInt("MinusDamage");
                } else if (collision.transform.parent.gameObject.GetComponent<HP>()) {
                    damage = collision.transform.parent.gameObject.GetComponent<HP>().damageToOther - PlayerPrefs.GetInt("MinusDamage");
                }
                if (damage <= 0)
                    damage = 1;
                if (maxHp >= 0) {
                    PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") - damage);
                    HitSoundEffect();
                    GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                    damageText.GetComponentInChildren<TextMeshPro>().text = (damage).ToString();
                    PushAway();
                    if (!isDead) {
                        maxHp = PlayerPrefs.GetInt("MaxHP");
                        hp = PlayerPrefs.GetInt("CurrentHP");
                        CheckHeart();
                    }
                }
            } else if (collision.gameObject.tag == "Enemy_Arm" && Shield == true) {
                GetComponent<AbilityManager>().RemoveShield();
                PushAwayWithOut();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Invincible == false) {
            if (collision.gameObject.name == "Fire_0" && Shield == false) {
                if (maxHp >= 0) {
                    PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") - 15);
                    HitSoundEffect();
                    GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                    damageText.GetComponentInChildren<TextMeshPro>().text = (15).ToString();
                    PushAway();
                    if (!isDead) {
                        maxHp = PlayerPrefs.GetInt("MaxHP");
                        hp = PlayerPrefs.GetInt("CurrentHP");
                        CheckHeart();
                    }
                }
            } else if (collision.gameObject.name == "Fire_0" && Shield == true) {
                GetComponent<AbilityManager>().RemoveShield();
                PushAwayWithOut();
            }
            if (collision.gameObject.name == "Bounds") {
                transform.position = StartPosition + new Vector3(0, 20, 0);
            }
            // Hit Ghost
            if (collision.gameObject.tag == "Enemy" && Shield == false) {
                damage = Random.Range(20, 25) - PlayerPrefs.GetInt("MinusDamage");
                if (damage <= 0)
                    damage = 1;

                if (maxHp >= 0) {
                    PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") - damage);
                    HitSoundEffect();
                    GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                    damageText.GetComponentInChildren<TextMeshPro>().text = (damage).ToString();
                    PushAway();
                    if (!isDead) {
                        maxHp = PlayerPrefs.GetInt("MaxHP");
                        hp = PlayerPrefs.GetInt("CurrentHP");
                        CheckHeart();
                    }
                }
            } else if (collision.gameObject.tag == "Enemy" && Shield == true) {
                GetComponent<AbilityManager>().RemoveShield();
                PushAwayWithOut();
            }

            if (collision.gameObject.name == "FireBall_0(Clone)") {
                if (!isDead) {
                    HitSoundEffect();
                    maxHp = PlayerPrefs.GetInt("MaxHP");
                    hp = PlayerPrefs.GetInt("CurrentHP");
                    CheckHeart();
                }
            }
            if (collision.gameObject.name == "Acid_Boss(Clone)") {
                if (maxHp >= 0) {
                    PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("CurrentHP") - 2);
                    HitSoundEffect();
                    GameObject damageText = Instantiate(FloatingTextPrefab, transform);
                    damageText.GetComponentInChildren<TextMeshPro>().text = (2).ToString();
                    if (!isDead) {
                        maxHp = PlayerPrefs.GetInt("MaxHP");
                        hp = PlayerPrefs.GetInt("CurrentHP");
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
    public void CheckHeart() {
        if (0 >= hp) {
            isDead = true;

            Heart[0].GetComponent<SpriteRenderer>().sprite = Inim[2];
            Heart[1].GetComponent<SpriteRenderer>().sprite = Inim[2];
            Heart[2].GetComponent<SpriteRenderer>().sprite = Inim[2];
            // When dead enemy
            OnDeadHero();
        } else if ( maxHp * 0.16f >= hp ) {
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

    // On hit action
    void PushAway() {
        if (rb == null || damage == 0)
            return;

        rb.AddForce(transform.up * 600f);
        StartCoroutine(hitAnimation());
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }

    void PushAwayWithOut() {
        rb.AddForce(transform.up * 800f);
        StartCoroutine(hitAnimation());
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }

    // Hit animation
    public IEnumerator hitAnimation() {
        yield return new WaitForSeconds(.2f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    // All actiunes when caracther dead
    public void OnDeadHero() {
        KillSoundEffect();
        // Lose Screen
        StartCoroutine(showPanel());
    }

    public IEnumerator showPanel() {
        yield return new WaitForSeconds(.6f);
        SetStatic();
        Lose.gameObject.SetActive(true);
        Lose.GetComponent<LoseRevive>().VerifyRevived();
    }

    public void SetStatic() {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // Set Rigidbody static
        Collider2D[] colliders = GetComponents<Collider2D>(); // Get all colliders of enemy
        foreach( Collider2D collider in colliders ) { // Disable all colliders from this enemy
            collider.enabled = false;
        }
    }

    public void Revive() {
        PlayerPrefs.SetInt("CurrentHP", PlayerPrefs.GetInt("MaxHP"));
        isDead = false;
        Invincible = true;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Set Rigidbody static
        Collider2D[] colliders = GetComponents<Collider2D>(); // Get all colliders of enemy
        foreach( Collider2D collider in colliders ) { // Disable all colliders from this enemy
            collider.enabled = true;
        }

        maxHp = PlayerPrefs.GetInt("MaxHP");
        hp = PlayerPrefs.GetInt("CurrentHP");

        StartCoroutine(OnRevive());

        // CheckHeart();
    }

    IEnumerator OnRevive() {
        yield return new WaitForSeconds(5f);
        Invincible = false;
    }

    private void OnParticleCollision(GameObject other) {
        List<ParticleCollisionEvent> events;
        events = new List<ParticleCollisionEvent>();
 
        ParticleSystem m_System = other.GetComponent<ParticleSystem>();
 
        ParticleSystem.Particle[] m_Particles;
        m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
 
        ParticlePhysicsExtensions.GetCollisionEvents(other.GetComponent<ParticleSystem>(), gameObject, events);
        foreach(ParticleCollisionEvent coll in events)
        {
            if(coll.intersection!=Vector3.zero)
            {
                int numParticlesAlive = m_System.GetParticles(m_Particles);
                // Check only the particles that are alive
                for (int i = 0; i < numParticlesAlive; i++)
                {
                    //If the collision was close enough to the particle position, destroy it
                    if (Vector3.Magnitude(m_Particles[i].position - coll.intersection) < 0.05f)
                    {
                        PlayerPrefs.SetInt("CurrentXP", PlayerPrefs.GetInt("CurrentXP") + 2);

                        m_Particles[i].remainingLifetime = -1; //Kills the particle
                        m_System.SetParticles(m_Particles); // Update particle system
                        break;
                    }
                }
            }
        }
    }
}
