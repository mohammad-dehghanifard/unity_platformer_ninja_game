using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audios;
    public static AudioController instance;
    public AudioSource backGeroundMusic;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayAudio(int soundPlayIndex) => audios[soundPlayIndex].Play();
}
