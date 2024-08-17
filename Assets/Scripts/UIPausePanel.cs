using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSFX;
    private void Start()
    {
        pauseBtn.onClick.AddListener(() => Pause());

        transform.Find("panel").Find("continueBtn").GetComponent<Button>().onClick.AddListener(() => UnPause());

        transform.Find("panel").Find("restartBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(buttonSFX);
            SceneManager.LoadScene("GameScene");
        });

        gameObject.SetActive(false);
    }

    private void Pause()
    {
        audioSource.PlayOneShot(buttonSFX);
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    private void UnPause()
    {
        audioSource.PlayOneShot(buttonSFX);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
