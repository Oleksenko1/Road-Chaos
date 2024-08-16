using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyRiser : MonoBehaviour
{
    public static DifficultyRiser Instance { get; private set; }

    [SerializeField] private float targetMultiplier = 2f;
    [SerializeField] private float transitionTime = 120f;

    private float refVel = 0;
    private float currentMultiplier = 0.5f;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Increases difficulty - over time
        if (currentMultiplier < targetMultiplier)
        {
            currentMultiplier = Mathf.SmoothDamp(currentMultiplier, targetMultiplier, ref refVel, transitionTime);
        }
    }

    public float GetCurrentMultiplier()
    {
        return currentMultiplier;
    }
}
