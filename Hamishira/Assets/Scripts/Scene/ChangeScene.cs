using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // For transition scene
    public Animator transition;
    public ParticleSystem particle;
    public Animator MusicAnim;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            if (PlayerPrefs.GetInt("LevelScene") == 0) { // From Hell to Earth
                StartCoroutine(LoadLevel(4));
            } else if (PlayerPrefs.GetInt("LevelScene") == 1) { // From Earth to Paradise
                StartCoroutine(LoadLevel(5));
            } else if (PlayerPrefs.GetInt("LevelScene") == 2) { // From Paradise to Finish Scene
                PlayerPrefs.SetInt("LockedSword" + 9, 1);
                PlayerPrefs.SetInt("IfFinished", 1);
                SetRecord();
                StartCoroutine(LoadLevel(2));
            }
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    // Ienumerator for delay transition scene
    public IEnumerator LoadLevel(int levelIndex) {
        particle.Play();
        particle.gameObject.GetComponent<EnterPortalParticle>().OnStart();
        MusicAnim.SetBool("FadeOut", true);
        yield return new WaitForSeconds(5f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(levelIndex);
    }

    void SetRecord() {
        if (PlayerPrefs.GetInt("RecordKilledMonsters") != 0) {
            if (PlayerPrefs.GetInt("KilledMonsters") > PlayerPrefs.GetInt("RecordKilledMonsters")) {
                PlayerPrefs.SetInt("RecordKilledMonsters", PlayerPrefs.GetInt("KilledMonsters"));
            }
        } else {
            PlayerPrefs.SetInt("RecordKilledMonsters", PlayerPrefs.GetInt("KilledMonsters"));
        }
    }
}
