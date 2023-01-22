using System;
using UnityEngine;

[Serializable]
public class Sound
{
    /*THIS SCRIPT IS CREATED TO USING WITH SOUND MANAGER, IT'S DOESN'T HAVE ANY OBJECT*/
    public SoundManager.SoundName soundName; // Name list of the sounds is in the SoundManager.
    public AudioClip clip; // Choose AudioClip in Unity
    [Range(0f, 1f)] public float volume; // Adjust volume in unity
    public bool loop; // Loop or not
    [HideInInspector] public AudioSource audioSource; // AudioSource is Hide: You can't see it on Inspector.
}