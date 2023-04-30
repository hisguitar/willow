using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT IN GAMEPLAY MANAGER SCENE
     AND PUT THIS SCRIPT TO IT*/
    public static SoundManager instance;

    [SerializeField] private Sound[] sounds;

    // List of sounds
    public enum SoundName
    {
        // List Music
        Memories, // Background music for SampleScene
        Alert,
        CollectEffect,
        JumpEffect,
        ThrowEffect,
        ShotEffect, // Impact of knife
        SlashEffect,
        Doom, // death, destruction, or some other terrible fate
        Click,
        GodIsGoodToMe, // Background music for MainMenu
        StabbedEffect,
        LoveIsPuuung, // Background music for EndCredit
    }

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
        {
            Destroy(this);
        }
    }
    
    // For setting the sound
    public void Play(SoundName soundName)
    {
        var sound = GetSound(soundName);
        if (sound.audioSource == null)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
        }
        sound.audioSource.Play();
    }

    private Sound GetSound(SoundName soundName)
    { return Array.Find(sounds, s => s.soundName == soundName); }
}