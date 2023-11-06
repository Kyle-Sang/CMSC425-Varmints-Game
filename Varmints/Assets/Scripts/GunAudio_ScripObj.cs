using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Audio/GunShot Audio")]
public class GunAudio : ScriptableObject
{

    public float volume = 1f;
    public AudioClip empty;


    public void playAudio(AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioSource.clip, volume);
    }

    public void emptyMag(AudioSource audioSource)
    {
        audioSource.PlayOneShot(empty, volume);
    } 

}
