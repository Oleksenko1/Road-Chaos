using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounterScript : MonoBehaviour
{
    public static ScoreCounterScript Instance;

    private float scoreDelay = 0.25f;

    private TextMeshProUGUI text;
    private bool isScoring = true;
    private int currentScore = 0;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        PlayerController.Instance.OnDied += StopScore;

        StartCoroutine(ScoreCoroutine());
    }

    IEnumerator ScoreCoroutine()
    {
        while(isScoring)
        {
            AddScore();
            yield return new WaitForSeconds(scoreDelay);
        }
    }
    private void AddScore()
    {
        currentScore++;
        text.text = currentScore.ToString();
    }
    private void StopScore()
    {
        isScoring = false;
    }
    public int GetScore()
    {
        return currentScore;
    }
}
