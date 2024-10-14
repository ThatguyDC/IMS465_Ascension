using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    [Header("Script Comms")]

    private PlayerTestScript PlayerScript;
    [SerializeField] private GameObject Player;



    [Header("Audio Sources")]

    [SerializeField] public AudioSource PlayerAudio;


    [Header("Audio Clips")]

    [Header("Sound Effects")]

    public AudioClip ObjectiveCompleteSound;
    
    public AudioClip[] CollectablePickupSounds;

    [Header("Music")]

    public AudioClip LevelMusic;


    void Start()
    {
        PlayerScript = Player.GetComponent<PlayerTestScript>(); //gets player script from player game object
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevelMusic()
    {
        PlayerAudio.PlayOneShot(LevelMusic, 1f);

    }

    public void ObjectiveReached()
    {
        PlayerAudio.PlayOneShot(ObjectiveCompleteSound, 1f);
    }

    public void CollectableObtained()
    {
        PlayerAudio.PlayOneShot(CollectablePickupSounds[0], 1f);

    }
}
