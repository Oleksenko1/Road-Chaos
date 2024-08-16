using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenuButtons : MonoBehaviour
{
    [SerializeField] private AudioClip btnSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator panelAnimator;
    private void Awake()
    {
        transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Invoke(nameof(PlayButton), 1f);
            audioSource.PlayOneShot(btnSFX);
            panelAnimator.SetTrigger("fadeIn");
        });

        transform.Find("exitBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Invoke(nameof(ExitBtn), 1f);
            audioSource.PlayOneShot(btnSFX);
        });
    }

    private void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ExitBtn()
    {
        Application.Quit();
    }
}
