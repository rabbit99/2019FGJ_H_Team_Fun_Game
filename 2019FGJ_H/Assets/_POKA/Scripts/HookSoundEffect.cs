using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HookSoundEffect : MonoBehaviour
{
    private AudioSource PlayWithHook;

    [SerializeField]
    private AudioClip[] WhenThrow;
    [SerializeField]
    private AudioClip[] WhenHit;

    private void Start()
    {
        PlayWithHook = GetComponent<AudioSource>();
    }

    public void PlayRandomWhenThrow()
    {
        AudioClip clip = WhenThrow[Random.Range(0, WhenThrow.Length)];

        PlayWithHook.PlayOneShot(clip);
    }

    public void PlayRandomWhenHit()
    {
        AudioClip clip = WhenHit[Random.Range(0, WhenHit.Length)];

        PlayWithHook.PlayOneShot(clip);
    }
}
