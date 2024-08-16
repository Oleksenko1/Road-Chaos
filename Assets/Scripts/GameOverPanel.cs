using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private float preferedCountSpeed = 3f; // Speed of counting
    [SerializeField] private Animator panelAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonSFX;

    private Transform panel;

    private TextMeshProUGUI scoreTxt;
    private TextMeshProUGUI highestScoreTxt;

    private float currentScore = 0;
    private int targetScore;
    private int highestScore = 0;

    private float actualSpeed;

    private bool isCounting = false;
    private void Awake()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);

        panel = transform.Find("panel");

        scoreTxt = panel.Find("scoreTxt").GetComponent<TextMeshProUGUI>();
        highestScoreTxt = panel.Find("highestScoreTxt").GetComponent<TextMeshProUGUI>();

        panel.Find("restartBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Invoke(nameof(GoToGame), 1f);
            audioSource.PlayOneShot(buttonSFX);
            panelAnimator.SetTrigger("fadeIn");
        });

        panel.Find("menuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Invoke(nameof(GoToMenu), 1f);
            audioSource.PlayOneShot(buttonSFX);
            panelAnimator.SetTrigger("fadeIn");
        });
    }
    private void Start()
    {
        PlayerController.Instance.OnDied += ShowPanelDelayed;

        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isCounting)
        {
            currentScore = Mathf.MoveTowards(currentScore, targetScore, actualSpeed * Time.deltaTime);
            UpdateText();

            if (currentScore == targetScore)
            {
                isCounting = false;

                ShowHighScore();
            }
        }
    }
    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    private void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    private void ShowPanelDelayed()
    {
        Invoke(nameof(ShowPanel), 1f);
    }
    private void ShowHighScore()
    {
        if(currentScore > highestScore)
        {
            highestScore = (int)currentScore;
            PlayerPrefs.SetInt("HighestScore", highestScore);
        }
        highestScoreTxt.text = highestScore.ToString();
    }
    private void ShowPanel()
    {
        gameObject.SetActive(true);
        targetScore = ScoreCounterScript.Instance.GetScore();

        actualSpeed = targetScore / preferedCountSpeed;
        if (actualSpeed < 10) actualSpeed = 10;

        isCounting = true;
    }
    private void UpdateText()
    {
        scoreTxt.SetText(currentScore.ToString("0"));
    }
}
