using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControll : MonoBehaviour
{
    private float slowTime = 0.2f;
    private float fixedTime;
    private bool isSlowed;
    private ParticleSystem InstantiateParticel;

    public ParticleSystem particle;

    public GameObject FX;

    void Start() {
        fixedTime = Time.fixedDeltaTime;

        if (gameObject.name == "Boss_2(Clone)") {
            InvokeRepeating("FireBall", 15f, 15f);
        } else if (gameObject.name == "Boss_1(Clone)") {
            InvokeRepeating("Acid", 15f, 15f);
        }
    }

    public void OnFinished() {
        // Particle Xp
        InstantiateParticel = Instantiate(particle, (transform.position + new Vector3(0, 1f, 0)), Quaternion.identity);
        InstantiateParticel.Play();
        InstantiateParticel.gameObject.GetComponent<EnterPortalParticle>().OnStart();
        // Start Slow Motion
        isSlowed = true;
        // For cancel Slow Motion
        StartCoroutine("cancelSlowMo");
    }

    void Update() {
        if (!isSlowed) {
            Time.timeScale = 1;
            Time.fixedDeltaTime = fixedTime;
        } else {
            Time.timeScale = slowTime;
            Time.fixedDeltaTime = slowTime * Time.deltaTime;
        }
    }

    void FireBall() {
        GameObject fx = Instantiate(FX, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
        fx.transform.localScale = transform.localScale;
        fx.GetComponent<FireBall>().WhereLook = (int)transform.localScale.x;
    }

    void Acid() {
        StartCoroutine(InstantiateAcid());
    }

    IEnumerator InstantiateAcid() {
        int n = 0;
        while (n < 7) {
            yield return new WaitForSeconds(.2f);
            var go = Instantiate(FX, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            Destroy(go, 6f);
            n++;
        }
    }

    IEnumerator cancelSlowMo() {
        yield return new WaitForSeconds(.8f);
        isSlowed = false;
        GetComponentInParent<GameManager>().FinishLevel();
    }
}
