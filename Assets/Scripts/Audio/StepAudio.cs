using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip[] step_sounds;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StepSound()
    {
        source.PlayOneShot(step_sounds[Random.Range(0, step_sounds.Length)]);
    }
}
