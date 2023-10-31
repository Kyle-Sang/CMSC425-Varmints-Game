using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "GunShot Audio")]
public class GunAudio : ScriptableObject
{

    public float volume = 1f;
    public AudioClip clip;


    public void playAudio(AudioSource audioSource)
    {

        audioSource.PlayOneShot(clip, volume);

    }

}
