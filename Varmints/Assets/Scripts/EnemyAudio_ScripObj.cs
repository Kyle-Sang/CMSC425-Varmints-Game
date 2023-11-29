using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Audio/Enemy Audio")]
public class Enemy_Audio : ScriptableObject
{

    public float volume = 1f;
    public AudioClip[] audioSources;
    private System.Random rand = new System.Random();

    // Will "Randomly" choose a noise from the list
    // of audiosources.
    public IEnumerator PlayAudio(AudioSource source)
    {
        int num, time;


        time = rand.Next(5, 15);

        while (true) {
            yield return new WaitForSeconds(time);
            num = rand.Next(audioSources.Length);
            source.PlayOneShot(audioSources[num], volume);

        }
    }

}
