using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float aditionalSpeed = 0;

    private float moveSpeed = 30;
    private DifficultyRiser difficulty;
    private AudioSource audioSource;
    private void Start()
    {
        moveSpeed = aditionalSpeed != 0 ? moveSpeed + aditionalSpeed : moveSpeed; // Applies additioanl speed

        difficulty = DifficultyRiser.Instance;

        if(audioClip != null) // Plays sound if this obstacle has one
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    private void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * moveSpeed * difficulty.GetCurrentMultiplier());

        if(transform.position.z <= -50)
        {
            Destroy(gameObject);
        }
    }
}
