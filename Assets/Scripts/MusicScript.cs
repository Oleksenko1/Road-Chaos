using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot snapshot;
    private void Start()
    {
        PlayerController.Instance.OnDied += QuiteMusic;
    }
    private void QuiteMusic()
    {
        snapshot.TransitionTo(1f);
    }
}
