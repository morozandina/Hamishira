using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ButtonController : MonoBehaviour, IUnityAdsListener
{
    // For changing image on click pause button
    public Sprite pressedPause, relassedPause;
    static bool flag;
    static GameObject thisPause;

    // For transition scene
    public Animator transition;
    public Animator MusicAnim;
    public AudioSource SwordSound;

    void Start() {
        if (Advertisement.isSupported) {
            Advertisement.Initialize("4152591", false);
        }
        // Set first sword unlocked
        PlayerPrefs.SetInt("LockedSword" + 0, 1);
        if (PlayerPrefs.GetInt("CurrentSword") < 1) {
            PlayerPrefs.SetInt("CurrentSword", 0);
        }

        flag = true;
    }

    // On pause
    public void Pause(GameObject PauseModal) {
        thisPause = EventSystem.current.currentSelectedGameObject;

        if (flag == true) {
            thisPause.GetComponent<Button>().image.sprite = pressedPause;
            PauseModal.gameObject.SetActive(true);
            Time.timeScale = 0f;
        } else {
            thisPause.GetComponent<Button>().image.sprite = relassedPause;
        }
        flag = !flag;
    }

    // On continue
    public void Continue(GameObject PauseModal) {
        Time.timeScale = 1.0f;
        PauseModal.gameObject.SetActive(false);

        // Change pause button
        thisPause.GetComponent<Button>().image.sprite = relassedPause;
        flag = !flag;
    }

    // On exit
    public void ExitFromPause() {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadLevel(0));
        flag = !flag;
    }

    public void ChangeSceneFromId(int levelIndex) {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadLevel(levelIndex));
    }

    // Main Menu
    public void StartGame() {
        StartCoroutine(LoadLevel(3));
    }

    // Ienumerator for delay transition scene
    public IEnumerator LoadLevel(int levelIndex) {
        SwordSound.pitch = Random.Range(0.9f, 1.1f);
        SwordSound.Play();
        if (MusicAnim)
            MusicAnim.SetBool("FadeOut", true);

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(levelIndex);
    }

    // Double reward
    public void DoubleReward() {
        Time.timeScale = 1.0f;
        if (Advertisement.IsReady("hamishiraDoubleReward")) {
            Advertisement.Show("hamishiraDoubleReward");
            Advertisement.AddListener(this);
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string surfacingId, ShowResult showResult) {
        if (showResult == ShowResult.Finished) {
            PlayerPrefs.SetInt("CoinKey", PlayerPrefs.GetInt("CoinKey") + PlayerPrefs.GetInt("LevelCollect"));
            PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);
            StartCoroutine(LoadLevel(3));
        }
        else if (showResult == ShowResult.Skipped) {}
        else if (showResult == ShowResult.Failed) {}
    }

    public void OnUnityAdsReady (string surfacingId) {}

    public void OnUnityAdsDidError (string message) {}

    public void OnUnityAdsDidStart (string surfacingId) {}
}
