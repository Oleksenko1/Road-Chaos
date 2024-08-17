using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot quiteSnapshot;
    [SerializeField] private AudioMixerSnapshot defaultSnapshot;

    private void Start()
    {
        PlayerController.Instance.OnDied += QuiteMusic;
        defaultSnapshot.TransitionTo(0.1f);
    }
    private void QuiteMusic()
    {
        quiteSnapshot.TransitionTo(1f);
    }
}
